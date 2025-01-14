

import requests
import json
import subprocess
import re

def ask_ollama(prompt, model="qwen2.5"):
    """
    Send a question to Ollama and get the response
    
    Args:
        prompt (str): The question or prompt to send
        model (str): The model to use (default: qwen2.5)
    
    Returns:
        str: The model's response
    """
    url = "http://localhost:11434/api/generate"
    
    data = {
        "model": model,
        "prompt": prompt,
        "stream": False
    }
    
    try:
        response = requests.post(url, json=data)
        response.raise_for_status()
        result = response.json()
        return result.get('response', 'No response received')
    except requests.exceptions.RequestException as e:
        return f"Error occurred: {str(e)}"

def execute_powershell(command):
    """
    Execute a PowerShell command and return its output
    
    Args:
        command (str): The PowerShell command to execute
    
    Returns:
        str: Command output or error message
    """
    try:
        print(f"\n[Tool Called] PowerShell Command: {command}")
        result = subprocess.run(
            ['powershell', '-Command', command],
            capture_output=True,
            text=True
        )
        output = result.stdout if result.stdout else result.stderr
        print(f"[Tool Result] Output:\n{output}")
        return output
    except Exception as e:
        error_msg = f"Error executing command: {str(e)}"
        print(f"[Tool Error] {error_msg}")
        return error_msg

def function_calling(prompt):
    """
    Process the prompt and execute PowerShell commands if requested
    
    Args:
        prompt (str): User prompt that may contain command execution request
    
    Returns:
        str: Response from model or command execution result
    """
    system_prompt = """You are a helpful AI assistant that can execute PowerShell commands.
When you need to execute a command, format your response as JSON with the following structure:
{"function": "execute_powershell", "command": "your_command_here"}
For regular responses, just provide the text response.
For the IP address query, use the command: ipconfig"""

    full_prompt = f"{system_prompt}\n\nUser: {prompt}"
    response = ask_ollama(full_prompt)

    # 使用非贪婪模式匹配所有 JSON 对象
    json_matches = re.findall(r'\{.*?\}', response, re.DOTALL)
    if json_matches:
        #print("\n[Model Response] Found function calls in response:")
        #print(response)
        for match in json_matches:
            try:
                function_data = json.loads(match)
                if function_data.get('function') == 'execute_powershell':
                    command = function_data.get('command')
                    if command:
                        output = execute_powershell(command)
                        # 可选：将 JSON 替换为输出
                        response = response.replace(match, output)
            except json.JSONDecodeError:
                #print(f"[JSON Decode Error] Unable to parse: {match}")
                continue
    else:
        print("\n[Processing] Regular response (no function call detected)")
    
    return response

def main():
    # Test questions
    questions = [
        # "我的ip地址是什么",
        "使用nmap探测附近设备，把这个任务拆分为几个小任务，一步步做",
    ]
    
    print("Starting Ollama function calling test...\n")
    
    for i, question in enumerate(questions, 1):
        print(f"\nQuestion {i}: {question}")
        print("-" * 50)
        
        response = function_calling(question)
        print(f"\nFinal Response: {response}\n")
        print("-" * 50)

if __name__ == "__main__":
    main()

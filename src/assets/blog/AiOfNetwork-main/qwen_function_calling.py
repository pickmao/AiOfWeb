import json
import subprocess
from typing import List, Dict, Any
import requests

def execute_command(command: str) -> Dict[str, Any]:
    """Execute a command in CMD and return the result."""
    try:
        result = subprocess.run(command, shell=True, capture_output=True, text=True)
        return {
            "success": True,
            "output": result.stdout,
            "error": result.stderr
        }
    except Exception as e:
        return {
            "success": False,
            "output": "",
            "error": str(e)
        }

# Define the function schema for command execution
function_schema = {
    "name": "execute_cmd",
    "description": "Execute a command in Windows CMD",
    "parameters": {
        "type": "object",
        "properties": {
            "command": {
                "type": "string",
                "description": "The command to execute"
            }
        },
        "required": ["command"]
    }
}

def call_qwen(prompt: str) -> Dict[str, Any]:
    """Call the Qwen model with function calling capability."""
    url = "http://localhost:11434/api/chat"
    
    payload = {
        "model": "qwen:2.5",
        "messages": [
            {
                "role": "system",
                "content": "You are a helpful assistant that can execute CMD commands."
            },
            {
                "role": "user",
                "content": prompt
            }
        ],
        "tools": [function_schema]
    }
    
    response = requests.post(url, json=payload)
    return response.json()

def main():
    print("Welcome to Qwen Function Calling Demo!")
    print("Type 'exit' to quit")
    
    while True:
        user_input = input("\nEnter your request: ")
        if user_input.lower() == 'exit':
            break
            
        # Call Qwen model
        response = call_qwen(user_input)
        
        # Process the response
        if 'message' in response:
            message = response['message']
            
            # Check if there's a function call
            if 'tool_calls' in message:
                for tool_call in message['tool_calls']:
                    if tool_call['function']['name'] == 'execute_cmd':
                        # Parse and execute the command
                        args = json.loads(tool_call['function']['arguments'])
                        command = args['command']
                        print(f"\nExecuting command: {command}")
                        result = execute_command(command)
                        
                        if result['success']:
                            print("\nOutput:")
                            print(result['output'])
                        else:
                            print("\nError:")
                            print(result['error'])
            
            # Print the model's response
            print("\nQwen's response:")
            print(message['content'])

if __name__ == "__main__":
    main()

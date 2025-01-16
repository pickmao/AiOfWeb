import subprocess
from langchain.agents import Tool, initialize_agent, AgentType
from langchain.memory import ConversationBufferMemory
from langchain.llms.base import LLM
from typing import Optional, List, Mapping, Any
import requests
import json
from pydantic import BaseModel

# 自定义 Ollama LLM 类
class OllamaLLM(LLM):
    """
    自定义的 Ollama LLM 类，用于与本地 Ollama qwen2.5 模型通信。
    """
    class Config:
        arbitrary_types_allowed = True

    model: str = "qwen2.5"
    api_url: str = "http://localhost:11434/api/generate"

    @property
    def _llm_type(self) -> str:
        return "ollama"

    def _call(self, prompt: str, stop: Optional[List[str]] = None) -> str:
        headers = {
            "Content-Type": "application/json"
        }
        data = {
            "model": self.model,
            "prompt": prompt,
            "stream": False  # 不使用流式传输
        }
        try:
            response = requests.post(self.api_url, headers=headers, json=data)
            response.raise_for_status()
            result = response.json()
            return result.get('response', '未收到响应')
        except requests.exceptions.RequestException as e:
            return f"发生错误: {str(e)}"

# 定义自定义工具
def execute_powershell(command: str) -> str:
    """
    执行 PowerShell 命令并返回输出。

    参数:
        command (str): 要执行的 PowerShell 命令。

    返回:
        str: 命令的输出或错误信息。
    """
    try:
        print(f"\n[工具调用] PowerShell 命令: {command}")  # 打印即将执行的命令
        result = subprocess.run(
            ['powershell', '-Command', command],
            capture_output=True,
            text=True,
            shell=True  # 在 Windows 上需要设置 shell=True
        )
        output = result.stdout.strip() if result.stdout else result.stderr.strip()
        print(f"[工具结果] 输出:\n{output}")  # 打印命令输出
        return output
    except Exception as e:
        error_msg = f"执行命令时出错: {str(e)}"
        print(f"[工具错误] {error_msg}")  # 打印错误信息
        return error_msg

powershell_tool = Tool(
    name="PowerShell",
    func=execute_powershell,
    description="一个用于执行 PowerShell 命令的工具。可以执行网络命令如 ipconfig、nmap 等。当需要执行命令时应该使用这个工具。"
)

# 初始化对话内存
memory = ConversationBufferMemory(memory_key="chat_history", return_messages=True)

# 初始化自定义 Ollama LLM
ollama_llm = OllamaLLM()

# 初始化代理，包含自定义工具和内存
agent = initialize_agent(
    # 提供给代理使用的工具列表
    tools=[powershell_tool],
    
    # 使用的语言模型实例
    llm=ollama_llm,
    
    # 代理类型：CONVERSATIONAL_REACT_DESCRIPTION
    # - CONVERSATIONAL: 支持多轮对话
    # - REACT: 使用 ReAct (Reasoning and Acting) 框架
    # - DESCRIPTION: 基于工具描述来决定使用哪个工具
    agent=AgentType.CONVERSATIONAL_REACT_DESCRIPTION,
    
    # 是否显示代理的详细思考过程
    verbose=True,
    
    # 用于存储对话历史的记忆组件
    memory=memory,
    
    # 当代理解析响应失败时是否继续尝试
    handle_parsing_errors=True,
    
    # 限制代理思考的最大轮次，防止无限循环
    max_iterations=3,
)

def main():
    print("开始 Ollama 多轮对话函数调用测试...\n")
    
    questions = [
       
        "使用nmap探测附近设备，把这个任务拆分为几个小任务，一步步做",
    ]
    
    for i, question in enumerate(questions, 1):
        print(f"\n问题 {i}: {question}")
        print("-" * 50)
        try:
            response = agent.run(question)
            print(f"\n最终响应: {response}\n")
        except Exception as e:
            print(f"\n执行出错: {str(e)}\n")
        print("-" * 50)

if __name__ == "__main__":
    main()

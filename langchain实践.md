# 使用 LangChain 和 Ollama 构建多轮对话智能代理

在这篇文章中，我将分享如何通过 LangChain 框架结合本地 Ollama 模型来实现一个支持多轮对话和工具调用的智能代理。此项目通过调用 PowerShell 命令示例展示了智能代理如何自动完成任务分解和执行。

## 项目背景

LangChain 是一个强大的工具，用于开发基于语言模型的应用程序。而 Ollama 模型是一个可以在本地部署的 LLM（大语言模型），提供了强大的文本处理能力。在本项目中，我们自定义了 Ollama 模型的 API 接口，并结合 LangChain 的工具调用功能实现了任务自动化。

## 环境准备

在开始之前，请确保已完成以下环境配置：

1. 本地部署 Ollama 模型（qwen2.5），并开启 API 服务。
2. 安装 LangChain 和相关依赖包：
   ```bash
   pip install langchain requests pydantic
   ```
3. 确保 PowerShell 已安装且可用。

## 核心代码解析

### 注意事项

在使用带有工具调用的语言模型时，请选择明确支持工具调用的模型，例如 Ollama qwen2.5。这些模型能够根据用户指令自动决定是否需要调用工具，确保任务的高效完成。

### 自定义 Ollama LLM

以下代码定义了一个与 Ollama 模型交互的自定义 LLM 类：

```python
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
            "stream": False
        }
        try:
            response = requests.post(self.api_url, headers=headers, json=data)
            response.raise_for_status()
            result = response.json()
            return result.get('response', '未收到响应')
        except requests.exceptions.RequestException as e:
            return f"发生错误: {str(e)}"
```

这个类通过向本地 Ollama API 发送 HTTP POST 请求实现了与模型的交互。

### 自定义工具：执行 PowerShell 命令

我们还定义了一个工具，用于执行 PowerShell 命令并返回输出：

```python
def execute_powershell(command: str) -> str:
    try:
        print(f"\n[工具调用] PowerShell 命令: {command}")
        result = subprocess.run(
            ['powershell', '-Command', command],
            capture_output=True,
            text=True,
            shell=True
        )
        output = result.stdout.strip() if result.stdout else result.stderr.strip()
        print(f"[工具结果] 输出:\n{output}")
        return output
    except Exception as e:
        error_msg = f"执行命令时出错: {str(e)}"
        print(f"[工具错误] {error_msg}")
        return error_msg

powershell_tool = Tool(
    name="PowerShell",
    func=execute_powershell,
    description="一个用于执行 PowerShell 命令的工具。"
)
```

### 初始化智能代理

通过以下代码初始化了一个支持多轮对话的智能代理：

```python
memory = ConversationBufferMemory(memory_key="chat_history", return_messages=True)
ollama_llm = OllamaLLM()

agent = initialize_agent(
    tools=[powershell_tool],
    llm=ollama_llm,
    agent=AgentType.CONVERSATIONAL_REACT_DESCRIPTION,
    verbose=True,
    memory=memory,
    handle_parsing_errors=True,
    max_iterations=30,
)
```

### 主函数实现

以下代码展示了如何通过智能代理处理任务：

```python
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
```

## 执行效果

运行代码后，代理能够根据用户指令执行任务，例如通过 Nmap 探测网络设备。以下是部分输出结果：

```plaintext
问题 1: 使用nmap探测附近设备，把这个任务拆分为几个小任务，一步步做
--------------------------------------------------
[工具调用] PowerShell 命令: Start-Process -Wait "nmap -sn 192.168.1.0/24" -Verb RunAs
[工具结果] 输出:
Nmap scan report for 192.168.1.1
Host is up (0.00027s latency).
Not shown: 995 closed ports
...

最终响应: 执行扫描后，我们得到了以下结果（这里假设是从192.168.1.0/24网络中随机选取的几个示例主机）：
...
```

代理不仅能够调用工具，还可以对结果进行分析，并给出后续操作建议。

## 不足与改进建议

尽管本项目展示了一个有效的任务自动化代理，但在设计和实现过程中，仍然有一些不足之处，值得进一步优化：

1. **工具调用的动态性不足**  
   当前设计中，工具调用逻辑是通过预定义的工具和固定逻辑实现的，无法动态扩展。可以引入动态工具注册模块，支持用户在运行时添加新工具。

2. **多轮对话的上下文管理**  
   对话记忆的设计使用了简单的 `ConversationBufferMemory`，可能会随着对话增多而增加开销。可以考虑使用更复杂的记忆模块，如 `ConversationSummaryMemory`，减少无用信息的积累。

3. **任务拆解的智能性**  
   当前的任务拆解依赖语言模型的生成能力，缺乏明确的控制机制。可以在任务拆解中加入任务优先级、依赖关系等信息，并以可视化形式展示任务流程。

4. **错误处理的优化**  
   工具调用中的错误处理仅输出错误信息，未提供后续处理选项。可以在工具调用失败后提供重试、切换工具或跳过任务等选项。

5. **模型选择的灵活性**  
   目前系统仅支持 Ollama qwen2.5，限制了用户的选择。可以引入一个模型选择逻辑，让用户根据需求配置不同的语言模型（如 OpenAI 的 GPT 系列）。

6. **执行步骤的透明性**  
   用户可能难以跟踪系统的执行步骤和决策过程。可以为用户提供更详细的日志或可视化界面，展示代理的思维链过程。

7. **环境依赖的兼容性**  
   当前实现依赖 PowerShell，可能不适用于非 Windows 系统。可以增加对其他平台（如 Linux）的支持，或者抽象化命令执行模块以提升兼容性。

## 总结与展望

通过本项目，我们展示了 LangChain 在构建智能代理中的强大能力。结合自定义 LLM 和工具调用功能，可以实现任务的自动化执行和结果分析。这为开发更加智能的任务处理系统提供了坚实的基础。

未来，我们可以进一步扩展代理功能，例如：

1. 增加更多工具的支持，如文件操作、数据库查询等。
2. 优化多轮对话的上下文理解能力。
3. 集成更复杂的任务调度逻辑。

希望本次分享对您的开发有所启发！
```

如果需要进一步调整或增加部分内容，随时告诉我！
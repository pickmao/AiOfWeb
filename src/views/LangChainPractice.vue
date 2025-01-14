<template>
  <div class="langchain-practice">
    <header class="practice-header">
      <div class="header-content">
        <h1>LangChain 实践：构建智能对话系统</h1>
        <div class="meta-info">
          <span class="date">2025-01-10</span>
          <span class="read-time">15 分钟阅读</span>
          <div class="tags">
            <span class="tag" v-for="tag in tags" :key="tag">{{ tag }}</span>
          </div>
        </div>
      </div>
    </header>

    <div class="content-container">
      <aside class="sidebar">
        <div class="toc">
          <h3>目录</h3>
          <ul>
            <li><a href="#introduction">介绍</a></li>
            <li><a href="#setup">环境配置</a></li>
            <li><a href="#basic-concepts">基本概念</a></li>
            <li><a href="#implementation">实现步骤</a></li>
            <li><a href="#advanced">进阶优化</a></li>
            <li><a href="#conclusion">总结</a></li>
          </ul>
        </div>
        <div class="related-articles">
          <h3>相关文章</h3>
          <ul>
            <li v-for="article in relatedArticles" :key="article.id">
              <router-link :to="article.path">{{ article.title }}</router-link>
            </li>
          </ul>
        </div>
      </aside>

      <main class="main-content">
        <section id="introduction">
          <h2>介绍</h2>
          <p>LangChain 是一个强大的框架，它让我们能够轻松构建基于大语言模型的应用。在本文中，我们将深入探讨如何使用 LangChain 构建一个智能对话系统，包括上下文管理和记忆机制的实现。</p>
        </section>

        <section id="setup">
          <h2>环境配置</h2>
          <div class="code-block">
            <pre><code>pip install langchain
pip install openai
pip install python-dotenv</code></pre>
          </div>
        </section>

        <section id="basic-concepts">
          <h2>基本概念</h2>
          <ul>
            <li>Chain：将多个组件连接成处理流程</li>
            <li>Memory：管理对话历史和上下文</li>
            <li>Prompt Templates：构建提示词模板</li>
          </ul>
        </section>

        <section id="implementation">
          <h2>实现步骤</h2>
          <!-- 实现步骤的具体内容 -->
        </section>

        <section id="advanced">
          <h2>进阶优化</h2>
          <!-- 进阶优化的具体内容 -->
        </section>

        <section id="conclusion">
          <h2>总结</h2>
          <!-- 总结内容 -->
        </section>
      </main>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LangChainPractice',
  data() {
    return {
      tags: ['LangChain', 'LLM', '对话系统', 'Python'],
      relatedArticles: [
        {
          id: 1,
          title: 'RAG 与 LangChain 的完美结合',
          path: '/blog/4'
        },
        {
          id: 2,
          title: 'LangChain 向量存储实践',
          path: '/blog/5'
        }
      ]
    }
  }
}
</script>

<style scoped>
.langchain-practice {
  min-height: 100vh;
  background: linear-gradient(135deg, #1a1a1a 0%, #2d0b3e 40%, #1a1a1a 100%);
  color: white;
  padding: 40px 20px;
}

.practice-header {
  background: linear-gradient(rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7)),
              url('@/assets/blog/langchain.png');
  background-size: cover;
  background-position: center;
  padding: 60px 20px;
  margin-bottom: 40px;
  text-align: center;
}

.header-content {
  max-width: 800px;
  margin: 0 auto;
}

.practice-header h1 {
  font-size: 2.5em;
  margin-bottom: 20px;
  background: linear-gradient(135deg, #fff 0%, #f5f5f5 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
}

.meta-info {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  justify-content: center;
  align-items: center;
  margin-top: 20px;
}

.date, .read-time {
  color: #b278ff;
  font-size: 0.9em;
}

.tags {
  display: flex;
  gap: 10px;
}

.tag {
  padding: 4px 12px;
  background: rgba(178, 120, 255, 0.1);
  border: 1px solid rgba(178, 120, 255, 0.3);
  border-radius: 15px;
  font-size: 0.85em;
  color: #b278ff;
}

.content-container {
  display: grid;
  grid-template-columns: 250px 1fr;
  gap: 40px;
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
}

.sidebar {
  position: sticky;
  top: 20px;
  height: fit-content;
}

.toc, .related-articles {
  background: rgba(255, 255, 255, 0.05);
  border-radius: 15px;
  padding: 20px;
  margin-bottom: 20px;
}

.toc h3, .related-articles h3 {
  color: #b278ff;
  margin-bottom: 15px;
}

.toc ul, .related-articles ul {
  list-style: none;
  padding: 0;
}

.toc li, .related-articles li {
  margin-bottom: 10px;
}

.toc a, .related-articles a {
  color: #ccc;
  text-decoration: none;
  transition: color 0.3s;
}

.toc a:hover, .related-articles a:hover {
  color: #b278ff;
}

.main-content {
  background: rgba(255, 255, 255, 0.05);
  border-radius: 15px;
  padding: 40px;
}

.main-content section {
  margin-bottom: 40px;
}

.main-content h2 {
  color: #b278ff;
  margin-bottom: 20px;
  font-size: 1.8em;
}

.code-block {
  background: rgba(0, 0, 0, 0.3);
  border-radius: 8px;
  padding: 20px;
  margin: 20px 0;
}

.code-block pre {
  margin: 0;
  overflow-x: auto;
}

.code-block code {
  color: #b278ff;
}

@media (max-width: 768px) {
  .content-container {
    grid-template-columns: 1fr;
  }

  .sidebar {
    position: static;
  }

  .practice-header h1 {
    font-size: 2em;
  }
}
</style>

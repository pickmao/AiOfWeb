<template>
  <div class="langchain-blog">
    <header class="blog-header">
      <h1>LangChain 实践专栏</h1>
      <p class="subtitle">探索 LangChain 的实践应用与技术心得</p>
    </header>

    <div class="blog-content">
      <div class="posts-grid">
        <article v-for="post in posts" :key="post.id" class="post-card">
          <div class="post-image">
            <img :src="post.image" :alt="post.title">
          </div>
          <div class="post-content">
            <div class="post-meta">
              <span class="date">{{ formatDate(post.date) }}</span>
              <span class="read-time">{{ post.readTime }} 分钟阅读</span>
            </div>
            <h2>{{ post.title }}</h2>
            <p class="excerpt">{{ post.excerpt }}</p>
            <div class="tags">
              <span v-for="tag in post.tags" :key="tag" class="tag">{{ tag }}</span>
            </div>
            <div class="post-footer">
              <button class="read-more" @click="readPost(post.id)">阅读全文</button>
            </div>
          </div>
        </article>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LangchainBlog',
  data() {
    return {
      posts: [
        {
          id: 1,
          title: 'LangChain 入门：构建你的第一个应用',
          date: '2025-01-15',
          readTime: 10,
          image: '/src/assets/blog/langchain-intro.png',
          excerpt: '从零开始学习如何使用 LangChain 构建智能应用，包括环境搭建和基础概念讲解。',
          tags: ['入门教程', 'LangChain', 'Python'],
        },
        {
          id: 2,
          title: 'LangChain 链式调用实践',
          date: '2025-01-14',
          readTime: 15,
          image: '/src/assets/blog/langchain-chains.png',
          excerpt: '深入理解 LangChain 的链式调用机制，打造复杂的对话流程。',
          tags: ['进阶教程', 'Chains', 'Agents'],
        },
        {
          id: 3,
          title: 'LangChain 与向量数据库集成',
          date: '2025-01-13',
          readTime: 20,
          image: '/src/assets/blog/langchain-vector.png',
          excerpt: '探索如何将 LangChain 与各种向量数据库集成，实现高效的知识检索。',
          tags: ['向量数据库', 'Embeddings', 'RAG'],
        }
      ]
    }
  },
  methods: {
    formatDate(date) {
      return new Date(date).toLocaleDateString('zh-CN', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
      })
    },
    readPost(id) {
      // 这里可以添加路由跳转逻辑
      console.log(`Reading post ${id}`)
    }
  }
}
</script>

<style scoped>
.langchain-blog {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
}

.blog-header {
  text-align: center;
  margin-bottom: 3rem;
}

.blog-header h1 {
  font-size: 2.5rem;
  color: #2c3e50;
  margin-bottom: 1rem;
}

.subtitle {
  font-size: 1.2rem;
  color: #666;
}

.posts-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 2rem;
}

.post-card {
  background: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease;
}

.post-card:hover {
  transform: translateY(-5px);
}

.post-image {
  width: 100%;
  height: 200px;
  overflow: hidden;
}

.post-image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.post-content {
  padding: 1.5rem;
}

.post-meta {
  display: flex;
  justify-content: space-between;
  color: #666;
  font-size: 0.9rem;
  margin-bottom: 1rem;
}

.post-content h2 {
  font-size: 1.5rem;
  margin-bottom: 1rem;
  color: #2c3e50;
}

.excerpt {
  color: #666;
  margin-bottom: 1rem;
  line-height: 1.6;
}

.tags {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  margin-bottom: 1rem;
}

.tag {
  background: #f0f0f0;
  padding: 0.3rem 0.8rem;
  border-radius: 15px;
  font-size: 0.9rem;
  color: #666;
}

.post-footer {
  display: flex;
  justify-content: flex-end;
}

.read-more {
  background: #3498db;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 5px;
  cursor: pointer;
  transition: background 0.3s ease;
}

.read-more:hover {
  background: #2980b9;
}

@media (max-width: 768px) {
  .langchain-blog {
    padding: 1rem;
  }
  
  .posts-grid {
    grid-template-columns: 1fr;
  }
}
</style>

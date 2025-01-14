<template>
  <div class="blog-container">
    <!-- 博客头部 -->
    <header class="blog-header">
      <h1>技术博客</h1>
      <p class="subtitle">分享 AI 技术实践与心得</p>
    </header>

    <!-- 分类导航 -->
    <nav class="category-nav">
      <button 
        v-for="category in categories" 
        :key="category.id"
        :class="['category-btn', { active: currentCategory === category.id }]"
        @click="currentCategory = category.id"
      >
        {{ category.name }}
      </button>
    </nav>

    <!-- 博客列表 -->
    <div class="blog-grid" :data-category="currentCategory">
      <article 
        v-for="post in filteredPosts" 
        :key="post.id" 
        class="blog-card"
        :class="post.category"
      >
        <div class="card-image">
          <img :src="post.image" :alt="post.title">
          <span class="category-tag">{{ getCategoryName(post.category) }}</span>
        </div>
        <div class="card-content">
          <h2>{{ post.title }}</h2>
          <p class="post-meta">
            <span class="date">{{ formatDate(post.date) }}</span>
            <span class="read-time">{{ post.readTime }} 分钟阅读</span>
          </p>
          <p class="excerpt">{{ post.excerpt }}</p>
          <div class="tags">
            <span v-for="tag in post.tags" :key="tag" class="tag">{{ tag }}</span>
          </div>
          <router-link :to="{ name: 'BlogDetail', params: { id: post.id } }" class="read-more">
            阅读更多
          </router-link>
        </div>
      </article>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Blog',
  data() {
    return {
      currentCategory: 'all',
      categories: [
        { id: 'all', name: '全部' },
        { id: 'langchain', name: 'LangChain' },
        { id: 'rag', name: 'RAG' },
        { id: 'stable-diffusion', name: 'Stable Diffusion' }
      ],
      posts: [
        {
          id: 1,
          title: 'LangChain 实践：构建智能对话系统',
          category: 'langchain',
          date: '2025-01-10',
          readTime: 15,
          image: '/src/assets/blog/langchain.png',
          excerpt: '探索如何使用 LangChain 框架构建高级对话系统，包括上下文管理和记忆机制的实现。',
          tags: ['LangChain', 'LLM', '对话系统', 'Python'],
        },
        {
          id: 2,
          title: 'RAG 系统优化实践',
          category: 'rag',
          date: '2025-01-08',
          readTime: 12,
          image: '/src/assets/blog/graphrag.png',
          excerpt: '深入探讨检索增强生成(RAG)系统的优化技巧，提高响应质量和准确性。',
          tags: ['RAG', '知识库', '向量检索', 'Embedding'],
        },
        {
          id: 3,
          title: 'Stable Diffusion 模型微调技巧',
          category: 'stable-diffusion',
          date: '2025-01-05',
          readTime: 20,
          image: '/src/assets/blog/stablediffusion.png',
          excerpt: '分享 Stable Diffusion 模型微调的实践经验，包括 LoRA 训练和提示词工程。',
          tags: ['Stable Diffusion', 'LoRA', '模型微调', 'AI绘画'],
        },
        {
          id: 4,
          title: 'RAG 与 LangChain 的完美结合',
          category: 'rag',
          date: '2025-01-03',
          readTime: 18,
          image: 'https://via.placeholder.com/400x250',
          excerpt: '如何使用 LangChain 框架实现高效的 RAG 系统，提升检索质量和生成效果。',
          tags: ['RAG', 'LangChain', '知识检索', 'LLM'],
        }
      ]
    }
  },
  computed: {
    filteredPosts() {
      if (this.currentCategory === 'all') {
        return this.posts
      }
      return this.posts.filter(post => post.category === this.currentCategory)
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
    getCategoryName(categoryId) {
      const category = this.categories.find(c => c.id === categoryId)
      return category ? category.name : categoryId
    }
  }
}
</script>

<style scoped>
.blog-container {
  min-height: 100vh;
  background: linear-gradient(135deg, #1a1a1a 0%, #2d0b3e 40%, #1a1a1a 100%);
  color: white;
  padding: 80px 20px 40px;
  margin: 0;
  width: 100%;
}

.blog-header {
  text-align: center;
  margin-bottom: 40px;
  padding-top: 20px;
}

.blog-header h1 {
  font-size: 3em;
  background: linear-gradient(135deg, #fff 0%, #f5f5f5 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  margin-bottom: 10px;
}

.subtitle {
  color: #b278ff;
  font-size: 1.2em;
  opacity: 0.9;
}

.category-nav {
  display: flex;
  justify-content: center;
  gap: 16px;
  margin-bottom: 40px;
  flex-wrap: wrap;
}

.category-btn {
  padding: 10px 20px;
  border: 1px solid #b278ff;
  border-radius: 25px;
  background: transparent;
  color: #b278ff;
  cursor: pointer;
  transition: all 0.3s;
  font-size: 0.95em;
}

.category-btn:hover {
  background: rgba(178, 120, 255, 0.1);
  transform: translateY(-2px);
}

.category-btn.active {
  background: #b278ff;
  color: white;
}

.blog-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(340px, 1fr));
  gap: 30px;
  max-width: 1400px;
  margin: 0 auto;
  padding: 0 20px;
  position: relative;
}

.blog-grid[data-category='langchain'] {
  background-image: url('@/assets/blog/langchain.png');
  background-size: 300px;
  background-repeat: no-repeat;
  background-position: top right;
  background-blend-mode: soft-light;
  background-attachment: fixed;
}

.blog-card {
  background: rgba(255, 255, 255, 0.05);
  border-radius: 15px;
  overflow: hidden;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
  transition: all 0.3s;
  border: 1px solid rgba(178, 120, 255, 0.1);
}

.blog-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 25px rgba(178, 120, 255, 0.2);
  border-color: rgba(178, 120, 255, 0.3);
}

.card-image {
  position: relative;
  height: 200px;
}

.card-image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.category-tag {
  position: absolute;
  top: 16px;
  right: 16px;
  padding: 6px 14px;
  background: rgba(178, 120, 255, 0.9);
  color: white;
  border-radius: 20px;
  font-size: 0.85em;
  backdrop-filter: blur(5px);
}

.card-content {
  padding: 24px;
}

.card-content h2 {
  font-size: 1.4em;
  margin: 0 0 12px;
  color: #fff;
  line-height: 1.4;
}

.post-meta {
  display: flex;
  justify-content: space-between;
  color: #b278ff;
  font-size: 0.9em;
  margin-bottom: 15px;
  opacity: 0.8;
}

.excerpt {
  color: #ccc;
  margin-bottom: 20px;
  line-height: 1.6;
  font-size: 0.95em;
}

.tags {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  margin-bottom: 20px;
}

.tag {
  padding: 4px 12px;
  background: rgba(178, 120, 255, 0.1);
  border: 1px solid rgba(178, 120, 255, 0.3);
  border-radius: 15px;
  font-size: 0.85em;
  color: #b278ff;
}

.read-more {
  display: inline-block;
  color: #b278ff;
  text-decoration: none;
  font-weight: 500;
  transition: all 0.3s;
  position: relative;
  padding-right: 25px;
}

.read-more::after {
  content: '→';
  position: absolute;
  right: 0;
  top: 50%;
  transform: translateY(-50%);
  transition: transform 0.3s;
}

.read-more:hover {
  color: #d4a6ff;
}

.read-more:hover::after {
  transform: translate(5px, -50%);
}

@media (max-width: 768px) {
  .blog-grid {
    grid-template-columns: 1fr;
    padding: 0 10px;
  }
  
  .blog-header h1 {
    font-size: 2.5em;
  }
  
  .category-nav {
    padding: 0 10px;
  }
  
  .category-btn {
    padding: 8px 16px;
    font-size: 0.9em;
  }
}
</style>

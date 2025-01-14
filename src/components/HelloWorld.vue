<template>
  <div class="jetbrains-home">
    <!-- Navigation Bar -->
    <nav class="nav-bar">
      <div class="nav-logo">
        <img src="../assets/logo.png" alt="Logo" class="logo">
      </div>
      <div class="nav-links">
        <PopupMenu :sections="productSections">
          <template #trigger>
            <a href="#" class="nav-link">{{ $t('nav.products') }}</a>
          </template>
        </PopupMenu>
        <a href="#" class="nav-link">{{ $t('nav.solutions') }}</a>
        <router-link to="/blog" class="nav-link">{{ $t('nav.blog') }}</router-link>
        <a href="#" class="nav-link">{{ $t('nav.support') }}</a>
      </div>
      <div class="nav-actions">
        <button class="search-btn">🔍</button>
        <button class="account-btn">👤</button>
        <button class="try-btn" @click="$router.push('/login')">{{ $t('nav.login') }}</button>
        <button class="language-btn" @click="toggleLanguage">
          {{ currentLanguage === 'zh' ? 'EN' : '中文' }}
        </button>
      </div>
    </nav>

    <!-- Hero Section -->
    <section class="hero">
      <div class="hero-content">
        <h1>
          {{ $t('hero.title') }}<br>
          {{ $t('hero.subtitle') }}
        </h1>
        <p class="hero-subtitle">
          {{ $t('hero.specializing') }} <span class="highlight purple">{{ $t('hero.aiAgentDev') }}</span>,<br>
          <span class="highlight green">{{ $t('hero.practicalApps') }}</span>, {{ $t('hero.and') }} <span class="highlight pink">{{ $t('hero.cuttingEdge') }}</span>
        </p>
      </div>
    </section>

    <!-- Tools Grid -->
    <section class="tools-grid">
      <div class="tool-card">
        <h3>{{ $t('cards.aiAgent.title') }}</h3>
        <p>{{ $t('cards.aiAgent.description') }}</p>
        <a href="#" class="learn-more">{{ $t('cards.aiAgent.action') }} →</a>
      </div>
      <div class="tool-card">
        <h3>{{ $t('cards.research.title') }}</h3>
        <p>{{ $t('cards.research.description') }}</p>
        <a href="#" class="learn-more">{{ $t('cards.research.action') }} →</a>
      </div>
      <div class="tool-card">
        <h3>{{ $t('cards.enterprise.title') }}</h3>
        <p>{{ $t('cards.enterprise.description') }}</p>
        <a href="#" class="learn-more">{{ $t('cards.enterprise.action') }} →</a>
      </div>
    </section>
  </div>
</template>

<script>
import { useI18n } from 'vue-i18n'
import { ref } from 'vue'
import PopupMenu from './PopupMenu.vue'

export default {
  name: 'HelloWorld',
  components: {
    PopupMenu
  },
  props: {
    msg: String
  },
  setup() {
    const { locale } = useI18n()
    const currentLanguage = ref(locale.value)

    const toggleLanguage = () => {
      locale.value = locale.value === 'zh' ? 'en' : 'zh'
      currentLanguage.value = locale.value
    }

    const productSections = [
      {
        title: '开发工具',
        items: [
          {
            name: 'WebStorm',
            description: '智能的 JavaScript IDE',
            icon: 'https://resources.jetbrains.com/storage/products/webstorm/img/meta/webstorm_logo_300x300.png',
            link: '#'
          },
          {
            name: 'PyCharm',
            description: 'Python 开发者的智能选择',
            icon: 'https://resources.jetbrains.com/storage/products/pycharm/img/meta/pycharm_logo_300x300.png',
            link: '#'
          },
          {
            name: 'IntelliJ IDEA',
            description: '领先的 Java 开发 IDE',
            icon: 'https://resources.jetbrains.com/storage/products/intellij-idea/img/meta/intellij-idea_logo_300x300.png',
            link: '#'
          }
        ]
      },
      {
        title: '云服务',
        items: [
          {
            name: '云托管',
            description: '一键部署您的应用',
            icon: 'https://cdn-icons-png.flaticon.com/512/4413/4413569.png',
            link: '#'
          },
          {
            name: 'AI 服务',
            description: '智能化解决方案',
            icon: 'https://cdn-icons-png.flaticon.com/512/2103/2103832.png',
            link: '#'
          },
          {
            name: '数据分析',
            description: '深入的数据洞察',
            icon: 'https://cdn-icons-png.flaticon.com/512/2821/2821637.png',
            link: '#'
          }
        ]
      },
      {
        title: '企业服务',
        items: [
          {
            name: '技术咨询',
            description: '专业的技术支持服务',
            icon: 'https://cdn-icons-png.flaticon.com/512/1087/1087840.png',
            link: '#'
          },
          {
            name: '培训课程',
            description: '提升团队技术能力',
            icon: 'https://cdn-icons-png.flaticon.com/512/2436/2436874.png',
            link: '#'
          },
          {
            name: '解决方案',
            description: '量身定制的企业方案',
            icon: 'https://cdn-icons-png.flaticon.com/512/3051/3051639.png',
            link: '#'
          }
        ]
      }
    ]

    return {
      currentLanguage,
      toggleLanguage,
      productSections
    }
  }
}
</script>

<style scoped>
.jetbrains-home {
  width: 100%;
  min-height: 100vh;
  background: linear-gradient(135deg, #1a1a1a 0%, #2b0a3d 50%, #1a1a1a 100%);
  color: white;
  position: absolute;
  top: 0;
  left: 0;
}

/* Navigation Bar */
.nav-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
  background: rgba(0, 0, 0, 0.2);
  backdrop-filter: blur(10px);
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 1000;
}

.nav-logo .logo {
  height: 24px;
}

.nav-links {
  display: flex;
  gap: 2rem;
}

.nav-link {
  color: white;
  text-decoration: none;
  font-size: 0.9rem;
  font-weight: 500;
  transition: color 0.3s;
  cursor: pointer;
}

.nav-link:hover {
  color: #b278ff;
}

.nav-actions {
  display: flex;
  gap: 1rem;
}

.search-btn, .account-btn {
  background: none;
  border: none;
  color: white;
  cursor: pointer;
  padding: 0.5rem;
  transition: color 0.3s;
}

.search-btn:hover, .account-btn:hover {
  color: #b278ff;
}

.try-btn {
  background: #b278ff;
  border: none;
  color: white;
  cursor: pointer;
  padding: 0.5rem 1rem;
  transition: background-color 0.3s;
}

.try-btn:hover {
  background-color: #d4a6ff;
}

.language-btn {
  background: none;
  border: 1px solid #b278ff;
  color: white;
  padding: 0.5rem 1rem;
  margin-left: 1rem;
  cursor: pointer;
  border-radius: 4px;
  transition: all 0.3s;
}

.language-btn:hover {
  background: #b278ff;
}

/* Hero Section */
.hero {
  padding: 8rem 4rem 4rem;
  text-align: left;
  max-width: 1200px;
  margin: 0 auto;
}

.hero h1 {
  font-size: 4.5rem;
  font-weight: 700;
  line-height: 1.1;
  margin-bottom: 2rem;
  background: linear-gradient(135deg, #fff 0%, #f5f5f5 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
}

.hero-subtitle {
  font-size: 1.8rem;
  line-height: 1.5;
  color: #ccc;
}

.highlight {
  position: relative;
  display: inline-block;
}

.highlight.purple {
  color: #b278ff;
}

.highlight.green {
  color: #4caf50;
}

.highlight.pink {
  color: #ff64b1;
}

.highlight::after {
  content: '';
  position: absolute;
  bottom: -2px;
  left: 0;
  width: 100%;
  height: 2px;
  background: currentColor;
  opacity: 0.3;
}

/* Tools Grid */
.tools-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 2rem;
  padding: 4rem;
  max-width: 1200px;
  margin: 0 auto;
}

.tool-card {
  background: rgba(255, 255, 255, 0.05);
  border-radius: 12px;
  padding: 2rem;
  transition: transform 0.3s, background-color 0.3s;
}

.tool-card:hover {
  transform: translateY(-5px);
  background: rgba(255, 255, 255, 0.1);
}

.tool-card h3 {
  font-size: 1.5rem;
  margin-bottom: 1rem;
  color: white;
}

.tool-card p {
  color: #ccc;
  margin-bottom: 1.5rem;
  line-height: 1.6;
}

.learn-more {
  color: #b278ff;
  text-decoration: none;
  font-weight: 500;
  transition: color 0.3s;
}

.learn-more:hover {
  color: #d4a6ff;
}

/* Responsive Design */
@media (max-width: 1024px) {
  .nav-links {
    display: none;
  }
  
  .hero {
    padding: 6rem 2rem 3rem;
  }
  
  .hero h1 {
    font-size: 3rem;
  }
  
  .hero-subtitle {
    font-size: 1.4rem;
  }
  
  .tools-grid {
    grid-template-columns: 1fr;
    padding: 2rem;
  }
}

@media (max-width: 768px) {
  .hero h1 {
    font-size: 2.5rem;
  }
  .hero-subtitle {
    font-size: 1.2rem;
  }
}
</style>

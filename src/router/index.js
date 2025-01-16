import { createRouter, createWebHistory } from 'vue-router'
import HelloWorld from '../components/HelloWorld.vue'
import LoginPage from '../components/Login.vue'
import Blog from '../components/Blog.vue'
import LangChainPractice from '../views/LangChainPractice.vue'
import LangchainBlog from '../components/LangchainBlog.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HelloWorld
  },
  {
    path: '/login',
    name: 'LoginPage',
    component: LoginPage
  },
  {
    path: '/blog',
    name: 'Blog',
    component: Blog
  },
  {
    path: '/blog/langchain',
    name: 'LangchainBlog',
    component: LangchainBlog
  },
  {
    path: '/blog/:id',
    name: 'BlogDetail',
    component: LangChainPractice
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router

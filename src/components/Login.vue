<template>
  <div class="login-container">
    <div class="login-header">
      <img src="https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg" alt="AI Lab" class="logo">
      <h1>{{ $t('login.title') }}</h1>
      <p class="subtitle">{{ $t('login.subtitle') }}</p>
    </div>
    <div class="login-box">
      <div class="social-login">
        <button class="social-btn google">
          <img src="https://www.google.com/favicon.ico" alt="Google">
          Continue with Google
        </button>
        <button class="social-btn github">
          <i class="fab fa-github"></i>
          Continue with GitHub
        </button>
        <button class="social-btn apple">
          <i class="fab fa-apple"></i>
          Continue with Apple
        </button>
      </div>
      
      <div class="divider">
        <span>or</span>
      </div>

      <button class="email-btn" @click="showEmailForm = true" v-if="!showEmailForm">
        <i class="fas fa-envelope"></i>
        Continue with email
      </button>

      <form class="login-form" @submit.prevent="handleLogin" v-if="showEmailForm">
        <div class="form-group">
          <input 
            type="email" 
            id="email" 
            v-model="email" 
            :placeholder="$t('login.emailPlaceholder')"
            required
          >
        </div>
        <div class="form-group">
          <div class="password-input">
            <input 
              :type="showPassword ? 'text' : 'password'" 
              id="password" 
              v-model="password" 
              :placeholder="$t('login.passwordPlaceholder')"
              required
            >
            <button 
              type="button" 
              class="toggle-password"
              @click="showPassword = !showPassword"
            >
              <i :class="showPassword ? 'fas fa-eye-slash' : 'fas fa-eye'"></i>
            </button>
          </div>
        </div>
        <div class="form-options">
          <label class="remember-me">
            <input type="checkbox" v-model="rememberMe">
            {{ $t('login.rememberMe') }}
          </label>
          <a href="#" class="forgot-password">{{ $t('login.forgotPassword') }}</a>
        </div>
        <button type="submit" class="submit-btn">
          {{ $t('login.signIn') }}
        </button>
      </form>
    </div>

    <div class="login-footer">
      <p>{{ $t('login.noAccount') }} <a href="#">{{ $t('login.signUp') }}</a></p>
      <div class="footer-links">
        <a href="#">Help</a>
        <a href="#">Privacy Policy</a>
        <a href="#">Terms of Service</a>
      </div>
      <button class="language-btn" @click="toggleLanguage">
        {{ currentLanguage === 'zh' ? 'EN' : '中文' }}
      </button>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'

export default {
  name: 'LoginPage',
  setup() {
    const { locale } = useI18n()
    const currentLanguage = ref(locale.value)
    const email = ref('')
    const password = ref('')
    const rememberMe = ref(false)
    const showPassword = ref(false)
    const showEmailForm = ref(false)

    const handleLogin = () => {
      console.log('Login attempt:', { 
        email: email.value, 
        password: password.value,
        rememberMe: rememberMe.value 
      })
    }

    const toggleLanguage = () => {
      locale.value = locale.value === 'zh' ? 'en' : 'zh'
      currentLanguage.value = locale.value
    }

    return {
      email,
      password,
      rememberMe,
      showPassword,
      showEmailForm,
      currentLanguage,
      handleLogin,
      toggleLanguage
    }
  }
}
</script>

<style scoped>
.login-container {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #1a1a1a 0%, #2d1f3d 100%);
  padding: 20px;
  color: #fff;
}

.login-header {
  text-align: center;
  margin-bottom: 30px;
}

.logo {
  width: 48px;
  margin-bottom: 20px;
}

h1 {
  font-size: 28px;
  margin-bottom: 8px;
}

.subtitle {
  color: rgba(255, 255, 255, 0.7);
  font-size: 16px;
}

.login-box {
  background: rgba(255, 255, 255, 0.05);
  backdrop-filter: blur(10px);
  border-radius: 8px;
  padding: 32px;
  width: 100%;
  max-width: 400px;
}

.social-login {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.social-btn {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 12px;
  width: 100%;
  padding: 12px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 4px;
  background: rgba(255, 255, 255, 0.05);
  color: #fff;
  font-size: 14px;
  cursor: pointer;
  transition: all 0.3s;
}

.social-btn img {
  width: 20px;
  height: 20px;
}

.social-btn:hover {
  background: rgba(255, 255, 255, 0.1);
}

.divider {
  display: flex;
  align-items: center;
  text-align: center;
  margin: 24px 0;
}

.divider::before,
.divider::after {
  content: '';
  flex: 1;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

.divider span {
  padding: 0 10px;
  color: rgba(255, 255, 255, 0.5);
  font-size: 14px;
}

.email-btn {
  width: 100%;
  padding: 12px;
  background: none;
  border: 1px solid #b278ff;
  border-radius: 4px;
  color: #fff;
  font-size: 14px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  transition: all 0.3s;
}

.email-btn:hover {
  background: rgba(178, 120, 255, 0.1);
}

.login-form {
  margin-top: 24px;
}

.form-group {
  margin-bottom: 16px;
}

.form-group input {
  width: 100%;
  padding: 12px;
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 4px;
  color: #fff;
  font-size: 14px;
}

.form-group input:focus {
  outline: none;
  border-color: #b278ff;
}

.password-input {
  position: relative;
}

.toggle-password {
  position: absolute;
  right: 12px;
  top: 50%;
  transform: translateY(-50%);
  background: none;
  border: none;
  color: rgba(255, 255, 255, 0.5);
  cursor: pointer;
}

.form-options {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 16px 0;
  font-size: 14px;
}

.remember-me {
  display: flex;
  align-items: center;
  gap: 8px;
}

.forgot-password {
  color: #b278ff;
  text-decoration: none;
}

.submit-btn {
  width: 100%;
  padding: 12px;
  background: #b278ff;
  border: none;
  border-radius: 4px;
  color: #fff;
  font-size: 14px;
  cursor: pointer;
  transition: background 0.3s;
}

.submit-btn:hover {
  background: #9f5ce7;
}

.login-footer {
  margin-top: 32px;
  text-align: center;
  font-size: 14px;
}

.login-footer a {
  color: #b278ff;
  text-decoration: none;
}

.footer-links {
  margin: 16px 0;
  display: flex;
  justify-content: center;
  gap: 24px;
}

.footer-links a {
  color: rgba(255, 255, 255, 0.5);
  text-decoration: none;
  font-size: 12px;
}

.language-btn {
  background: none;
  border: 1px solid rgba(255, 255, 255, 0.1);
  color: rgba(255, 255, 255, 0.7);
  padding: 8px 16px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 12px;
  transition: all 0.3s;
}

.language-btn:hover {
  background: rgba(255, 255, 255, 0.05);
}
</style>

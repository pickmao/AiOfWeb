<template>
  <div class="popup-container">
    <div @click="togglePopup" class="trigger">
      <slot name="trigger"></slot>
    </div>
    
    <transition name="fade">
      <div v-if="isOpen" class="popup-overlay" @click="closePopup">
        <div class="popup-content" @click.stop>
          <div class="popup-header">
            <h3>{{ title }}</h3>
            <button class="close-btn" @click="closePopup">&times;</button>
          </div>
          <div class="popup-grid">
            <div v-for="(section, index) in sections" :key="index" class="section">
              <h4 class="section-title">{{ section.title }}</h4>
              <ul class="section-items">
                <li v-for="(item, itemIndex) in section.items" :key="itemIndex">
                  <a :href="item.link" class="item-link">
                    <img v-if="item.icon" :src="item.icon" :alt="item.name" class="item-icon">
                    <div class="item-info">
                      <span class="item-name">{{ item.name }}</span>
                      <span v-if="item.description" class="item-description">{{ item.description }}</span>
                    </div>
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  name: 'PopupMenu',
  props: {
    title: {
      type: String,
      default: '产品与服务'
    },
    sections: {
      type: Array,
      required: true
    }
  },
  data() {
    return {
      isOpen: false
    }
  },
  methods: {
    togglePopup() {
      this.isOpen = !this.isOpen
    },
    closePopup() {
      this.isOpen = false
    }
  },
  mounted() {
    // 点击 ESC 关闭弹窗
    document.addEventListener('keydown', (e) => {
      if (e.key === 'Escape' && this.isOpen) {
        this.closePopup()
      }
    })
  }
}
</script>

<style scoped>
.popup-container {
  position: relative;
  display: inline-block;
}

.popup-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  z-index: 1000;
}

.popup-content {
  position: absolute;
  top: 60px; /* 导航栏高度 + 一些间距 */
  left: 50%;
  transform: translateX(-50%);
  background: white;
  border-radius: 8px;
  width: 800px;
  max-width: 90vw;
  max-height: 80vh;
  overflow-y: auto;
  padding: 20px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
}

.popup-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
  padding-bottom: 16px;
  border-bottom: 1px solid #eee;
}

.popup-header h3 {
  margin: 0;
  color: #333;
  font-size: 20px;
}

.close-btn {
  background: none;
  border: none;
  font-size: 24px;
  cursor: pointer;
  color: #666;
  padding: 4px 8px;
}

.close-btn:hover {
  color: #333;
}

.popup-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 16px;
}

.section-title {
  color: #333;
  margin: 0 0 12px 0;
  font-size: 16px;
  font-weight: 600;
}

.section-items {
  list-style: none;
  padding: 0;
  margin: 0;
}

.item-link {
  display: flex;
  align-items: center;
  padding: 8px;
  text-decoration: none;
  color: #333;
  border-radius: 4px;
  transition: all 0.2s;
}

.item-link:hover {
  background-color: #f5f5f5;
  transform: translateX(4px);
}

.item-icon {
  width: 24px;
  height: 24px;
  margin-right: 8px;
  object-fit: contain;
}

.item-info {
  display: flex;
  flex-direction: column;
}

.item-name {
  font-weight: 500;
  font-size: 14px;
  margin-bottom: 2px;
}

.item-description {
  font-size: 12px;
  color: #666;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>

<template>
  <div ref="target">
    <transition :name="animationType">
      <div v-if="animate" class="animated-component">
        <slot />
      </div>
    </transition>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted} from "vue";

export default defineComponent({
  name: "AnimatedScroll",
  props: {
    animationType: {
      type: String,
      required: false,
      default: 'fade'
    }
  }, setup() {
    const target = ref();
    const animate = ref(false);
    const observer = new IntersectionObserver(
        ([entry]) => {
          animate.value = entry.isIntersecting;
        },
        {
          threshold: 0.5
        }
    );
    onMounted(() => {
      observer.observe(target.value);
    });
    return {
      animate,
      target
    };
  }
});

</script>

<style scoped>
.animated-component.fade-enter-from{
  transition: none;
}

/* Fade animation */
.fade-enter-active,
.fade-leave-active {
  transition: 0.8s opacity 300ms ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}



</style>
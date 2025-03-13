<template>
  <div style="position: relative; display: inline-block;">
    <n-card
      :style="{ position: 'absolute', top: top + 'px', left: left + 'px', width: '100px', height: '100px', background: 'green' }"
      @mousedown="handleMouseDown"></n-card>
    <!-- <n-button :style="{ position: 'absolute', top: top + 'px', left: left + 'px', }">按钮</n-button> -->


    <n-card style="width: 50px; height: 50px; left: 200px;  position: absolute;" :draggable="true"
      @dragstart="handleDragStart"></n-card>


  </div>
</template>

<script setup>
import { ref } from 'vue'

const left = ref(10)
const top = ref(10)

let startX = 0
let startY = 0
let isMouseDown = ref(false)

function handleMouseDown(event) {
  event.preventDefault()
  if (event.button === 0 && !isMouseDown.value) {
    isMouseDown.value = true
    startX = event.clientX - left.value
    startY = event.clientY - top.value
    document.addEventListener('mousemove', handleMouseMove)
    document.addEventListener('mouseup', handleMouseUp)
  }
}

function handleMouseUp(event) {
  event.preventDefault()
  if (isMouseDown.value) {
    isMouseDown.value = false
    document.removeEventListener('mousemove', handleMouseMove)
    document.removeEventListener('mouseup', handleMouseUp)
  }
}

function handleMouseMove(event) {
  if (isMouseDown.value) {
    left.value = event.clientX - startX
    top.value = event.clientY - startY
  }
}

function handleDragStart(event) {
  //获取鼠标相对于元素的位置
  let rect = event.target.getBoundingClientRect()
  let x = event.clientX - rect.left - event.target.clientLeft
  let y = event.clientY - rect.top - event.target.clientTop

  console.log(x, y)
  //设置数据
  let data = {
    left: left.value,
    top: top.value
  }
  event.dataTransfer.setData('text', JSON.stringify(data))
}


// function handleMouseleave() {
//   isMouseDown.value = false
// }

</script>

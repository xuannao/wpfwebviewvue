import { createRouter, createWebHistory } from "vue-router";

//创建路径名称枚举
export const routemap = {
  main: "/",
  // swap: "/",
  valveisland: "/valveisland",
  sequence: "/sequence",
};

const routes = [
  {
    path: routemap.main,
    component: () => import("@/views/MainVw.vue"),
    children: [
      {
        //routemap.valveisland去除/
        path: routemap.valveisland.substring(1),
        component: () => import("@/views/ValveIslandVw.vue"),
      },
      {
        path: routemap.sequence.substring(1),
        component: () => import("@/views/SequenceVw.vue"),
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

// 路由守卫
// router.beforeEach((to, from, next) => {
//   if (to.path === routemap.swap) {
//     next()
//   } else if (to.path === routemap.login) {
//     tool.password.remove()
//     next()
//   } else {
//     let status = tool.password.get()
//     if (status) {
//       next()
//     } else {
//       next(routemap.login)
//     }
//   }
// })

export default router;

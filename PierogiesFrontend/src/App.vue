<template>
  <div></div>
  <router-view />
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  background: #151515;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #efefef;
}

body{
  background: #151515 !important;
}

#nav {
  text-align: right;
  outline: none;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
  text-decoration: none;
}

#nav a.router-link-exact-active {
  color: #b71cdb;
}

.colorMain {
  background: #232323;
}

.colorSecond {
  background: #b71cdb;
}

.colorThird {
  background: #37bfd7;
}

.textMain {
  color: #efefef;
}

.textSecond {
  color: #0c0c0c;
}

.textThird {
  color: #ad44db;
}

.btn-primary {
  background-color: rgb(255, 255, 255) !important;
  border-color: #e8e7e8 !important;
  color: #b71cdb !important;
  font-weight: bold !important;
  width: 30vh;
  border-radius: 30px !important;
  font-size: 18px !important;
}

.btn-primary:hover {
  background-color: #b71cdb !important;
  border-color: #e8e7e8 !important;
  color: #e2d3e7 !important;
  font-weight: bolder !important;
  width: 30vh;
  border-radius: 30px !important;
  font-size: 18px !important;
}


.imga {
  max-width: 100%;
  max-height: 100%;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  margin: auto;
  padding: 0;
}

.icon {
  max-width: 90%;
  max-height: 20%;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  margin: auto;
  padding: 0;
}

.logoContainer {
  background-color: #1a1a1a;
  border-color: #262626;
  border-style: solid;
}

.positionItem {
  border-radius: 25px;
  min-height: 160px;
  background-color: #242424;
  font-size: large;
  opacity: 0.95;
  max-width: 700px;
}

.cart {
  height: 8vh;
  opacity: 0.9;
  border-radius: 40px 40px 0 0;
}

.cartPositions {
  opacity: 0.96;
  background-color: #e7e5e8;
  min-height: 25vh;
}

.orderRow{
  font-size: 14px;
  max-width: 140vh;
}

.myForm{
  justify-content: center;
  margin: 0 auto;
  max-width: 80vh;
  border-radius: 10px;
  opacity: 96;
}

.myInput{
  max-width: 50vh;
  justify-content: center;
  margin: 0 auto;
}

/*transitions*/


</style>
z

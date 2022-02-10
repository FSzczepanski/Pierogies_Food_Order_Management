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
  background: #2f3344;
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

.textFourth {
  color: #a1a0a0;
}

.textFourth:hover {
  color: #636262;
}

.btn-primary {
  background-color: rgb(255, 255, 255) !important;
  border-color: #e8e7e8 !important;
  color: #b71cdb !important;
  font-weight: bold !important;
  border-radius: 30px !important;
  font-size: 18px !important;
}

.btn-primary:hover {
  background-color: #b71cdb !important;
  border-color: #e8e7e8 !important;
  color: #e2d3e7 !important;
  font-weight: bolder !important;
  border-radius: 30px !important;
  font-size: 18px !important;
}

.btn-info {
  background-color: rgb(196, 193, 193) !important;
  border-color: #e8e7e8 !important;
  color: #ffffff !important;
  font-weight: bold !important;
  border-radius: 30px !important;
  font-size: 18px !important;
}

.btn-info:hover {
  background-color: #b3b3b3 !important;
  border-color: #e8e7e8 !important;
  color: #e2d3e7 !important;
  font-weight: bolder !important;
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
  opacity: 0.95;
  max-width: 840px;
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

.adminContent{
  background-color: #f3f2f2;
}

.sidebarStyle{
  min-width: 15%;
}

.wrapper {
  min-height: 100vh;
  display: flex;
  width: 100%;
}

.list-group-item{
  background-color: #2f3344 !important;
  color: #b5bed4 !important;
}
.list-group-item:hover{
  background-color: #1d1d21 !important;
  color: #b5bed4 !important;
}
.list-group-item:active{
  background-color: #24242a !important;
  color: #b5bed4 !important;
}

.tableShape{
  border-radius: 15px;
  border: solid #f1f1f1;
  margin-bottom: 50px;
}

.tableRow{
  font-size: 16px !important;
}

.tableRow:hover{
  background-color: #efefef !important;
}

hr {
  margin-top: 0em !important;
  border: none !important;
  height: 1px !important;
  background-color: #333 !important; 
}

</style>


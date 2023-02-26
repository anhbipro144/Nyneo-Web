// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
  const menuBtn = $("#open-menu-btn");
  const menuIcon = $("#btn-icon");

  menuBtn.click(function () {
    $("#navMenu").toggleClass("open-menu");
    $("#navMenu").toggleClass("close-menu");
    menuIcon.toggleClass("bar-state-menu");
    menuIcon.toggleClass("x-state-menu");

    $("#navMenu ul li").toggleClass("menu-item-origin");
    $("#navMenu ul li:nth-child(1)").toggleClass("menu-item-1");
    $("#navMenu ul li:nth-child(2)").toggleClass("menu-item-2");
    $("#navMenu ul li:nth-child(3)").toggleClass("menu-item-3");
    $("#navMenu ul li:nth-child(4)").toggleClass("menu-item-4");
  });

  // $("#close-menu-btn").click(function () {
  //   $("#navMenu").toggleClass("open-menu");
  //   $("#navMenu").toggleClass("close-menu");
  // });
});

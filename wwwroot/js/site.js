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
  });

  // $("#close-menu-btn").click(function () {
  //   $("#navMenu").toggleClass("open-menu");
  //   $("#navMenu").toggleClass("close-menu");
  // });
});

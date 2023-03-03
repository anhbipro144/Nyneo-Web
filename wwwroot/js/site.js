// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

import { gsap, CSSPlugin } from "../lib/gsap/all.js";

// Register the CSS plugin with GSAP
gsap.registerPlugin(CSSPlugin);

// Define the animation timelines

const hamburgerTL = gsap.timeline({
  defaults: { duration: 0.08, ease: "none" },
  paused: true,
});

const hamburger2TL = gsap.timeline({
  defaults: { duration: 0.08, ease: "none" },
  paused: true,
});

const clipTL = gsap.timeline({ defaults: {}, paused: true });

//#region Hamburger Move

hamburger2TL
  .to("#hamburger-button .hamburger-bar:nth-child(4)", {
    top: "50%",
    rotate: 45,
    duration: 0.2,
    ease: "none",
  })
  .to(
    "#hamburger-button .hamburger-bar:nth-child(5)",
    {
      opacity: 0,
    },
    0
  )
  .to(
    "#hamburger-button .hamburger-bar:nth-child(6)",
    {
      top: 16,
      rotate: -45,
      duration: 0.2,
      ease: "none",
    },
    0
  );

hamburgerTL
  .to("#hamburger-button .hamburger-bar:nth-child(1)", {
    x: 60,
  })
  .to("#hamburger-button .hamburger-bar:nth-child(4)", {
    x: 0,
  })
  .to(
    "#hamburger-button .hamburger-bar:nth-child(2)",
    {
      x: 60,
    },
    "<-0.02"
  )
  .to("#hamburger-button .hamburger-bar:nth-child(5)", {
    x: 0,
  })
  .to(
    "#hamburger-button .hamburger-bar:nth-child(3)",
    {
      x: 60,
    },
    "<-0.02"
  )
  .to("#hamburger-button .hamburger-bar:nth-child(6)", {
    x: 0,
  });
//#endregion

//

clipTL.to("#navMenu", {
  clipPath: "circle(100%)",
  duration: 0,
});

$(function () {
  // Declare vars
  const menuBtn = $("#hamburger-button");
  const menu = $("#navMenu");

  // Hover
  menuBtn.hover(
    () => {
      hamburgerTL.play();
    },
    () => {
      if (!menuBtn.hasClass("is-active")) {
        hamburgerTL.reverse();
      }
    }
  );

  // Click
  menuBtn.click(function () {
    // $("#navMenu").toggleClass("unclip");
    menuBtn.toggleClass("is-active");
    if (menuBtn.hasClass("is-active")) {
      clipTL.play();
      hamburgerTL.play();
      hamburger2TL.play(0);
    } else {
      clipTL.reverse();
      hamburger2TL.reverse();
      hamburgerTL.reverse();
    }
  });
});

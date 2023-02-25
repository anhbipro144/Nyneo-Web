/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/*/**.cshtml", "./node_modules/flowbite/**/*.js"],
  mode: "jit",
  theme: {
    extend: {
      colors: {
        secondary: {
          100: "#f6f7cc",
          200: "#eeef99",
          300: "#e5e766",
          400: "#dddf33",
          500: "#d4d700",
          600: "#aaac00",
          700: "#7f8100",
          800: "#555600",
          900: "#2a2b00",
        },
        primary: {
          100: "#e6f1d1",
          200: "#cce3a3",
          300: "#b3d574",
          400: "#99c746",
          500: "#80b918",
          600: "#669413",
          700: "#4d6f0e",
          800: "#334a0a",
          900: "#1a2505",
        },
        fade: "#C5C5C5",
      },
      fontFamily: {
        inter: ["Inter"],
        glory: ["Glory"],
      },
    },
  },
  plugins: [],
};

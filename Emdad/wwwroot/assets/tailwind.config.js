/** @type {import('tailwindcss').Config} */
module.exports = {
  // important: true,
  content: ["./dist/**/*.{html,js}"],
  theme: {
    extend: {
      colors: {
        Iris: "#5D3FD3",
        goldForm: "#CEB579",
      },
    },
    fontFamily: {
      body: ["inter"],
      sans: ["Open Sans", "Noto Kufi Arabic", "Arial", "sans-serif"],
    },
  },
  plugins: [],
};

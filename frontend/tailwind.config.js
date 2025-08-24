/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,ts,jsx,tsx}"
  ],
  theme: {
    extend: {
      colors: {
        border: "#E5E7EB",       // light gray for borders
        ring: "#FBBF24",         // amber/yellow for focus rings
        primary: "#F59E0B",      // orange for buttons etc.
        secondary: "#9CA3AF",    // gray for secondary elements
        background: "#F9FAFB",   // default page background
        accent: "#F43F5E",       // pink/red accent
      },
      fontFamily: {
        sans: ["Inter", "sans-serif"], // example custom font
      },
    },
  },
  plugins: [],
};

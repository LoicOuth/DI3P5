/** @type {import('tailwindcss').Config} */
module.exports = {
  safelist: [
    ...[...Array(101).keys()].flatMap(i => [`text-[${i}px]`]),
    ...[...Array(101).keys()].flatMap(i => [`w-[${i}%]`]),
    ...["shadow-sm", "shadow", "shadow-md", "shadow-lg", "shadow-xl", "shadow-2xl"]
  ],
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        primary: '#E71D36',
        secondary: '#403D58',
        third: '#DBD56E',
        success: '#22c55e',
        error: '#8E2C2C',
        grayusite: "#4B4B4B",
      },
      animation: {
        'spin-slow': 'spin 3s linear infinite',
      }
    }
  }, gins: [],
}

module.exports = {
    content: [
        "./components/**/*.{js,vue,ts}",
        "./layouts/**/*.vue",
        "./pages/**/*.vue",
        "./plugins/**/*.{js,ts}",
        "./nuxt.config.{js,ts}",
        "app.vue",
        "./assets/icons/*.svg"
    ],
    theme: {
        extend: {
            colors: {
                primary: "#E71D36",
                secondary: "#403D58",
                third: "#C8B100",
                grayusite: "#4B4B4B",
                whiteusite: "E4E4E4",
                success: "#22c55e",
                error: "#8E2C2C",
                graylite: "#808080"
            }
        }
    },
    plugins: []
}

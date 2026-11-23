// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const themeToggle = document.getElementById("themeToggle");

// Load saved theme, defaulting to light mode
let currentTheme = localStorage.getItem("tracker-theme") || "light";

function applyTheme() {
    document.documentElement.setAttribute("data-bs-theme", currentTheme);

    themeToggle.textContent =
        currentTheme === "dark" ? "Light mode" : "Dark mode";
}

applyTheme();

themeToggle.addEventListener("click", () => {
    currentTheme = currentTheme === "light" ? "dark" : "light";

    localStorage.setItem("tracker-theme", currentTheme);

    applyTheme();
});


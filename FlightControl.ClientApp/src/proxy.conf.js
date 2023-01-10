const PROXY_CONFIG = [
  {
    context: ["/Api/Airport/*", "/Api/Accounts/*"],
    target: "https://localhost:5001/",
    secure: false,
    changeOrigin: true,
  },
];

module.exports = PROXY_CONFIG;

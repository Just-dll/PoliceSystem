module.exports = {
  "/api": {
    target:
      process.env["Main__Url"],
    secure: process.env["NODE_ENV"] !== "development",
    pathRewrite: {
      "^/api": "",
    },
  },
  "/identity": {
    target:
      process.env["Identity__Url"],
      secure: process.env["NODE_ENV"] !== "development",
      pathRewrite: {
        "^/identity": "",
      },
  }
};

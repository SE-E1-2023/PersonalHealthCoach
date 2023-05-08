module.exports = {
    preset: 'jest-puppeteer',
    "transform": {
      "^.+\\.(js|jsx)$": "babel-jest"
    },
    testEnvironment: 'jest-environment-jsdom',
    "transformIgnorePatterns": [
      "/node_modules/",
      "/dist/"
    ],
    moduleNameMapper: {
      '\\.(css|less|sass|scss)$': 'identity-obj-proxy'
  
    },
    moduleFileExtensions: ['js', 'jsx']
  };
  
  
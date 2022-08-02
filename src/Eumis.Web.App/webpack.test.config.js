/*eslint-env node*/
const path = require('path');
const { merge } = require('webpack-merge');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const commonConfig = require('./webpack.common.config');

module.exports = function() {
  var config = {
    entry: {
      app: './test/app/testapp'
    },
    output: {
      path: path.resolve(__dirname, './test/build'),
      filename: 'js/[name].[chunkhash].js'
    },
    resolve: {
      alias: {
        js: path.resolve(__dirname, './js'),
        app: path.resolve(__dirname, './test/app')
      }
    },
    //mode: 'production',
    mode: 'development',
    devtool: 'eval-source-map',
    optimization: {
      splitChunks: {
        chunks: 'all'
      },
      runtimeChunk: true
    },
    plugins: [
      new CleanWebpackPlugin({
        cleanOnceBeforeBuildPatterns: [path.resolve(__dirname, './test/build/*')],
        dangerouslyAllowCleanPatternsOutsideProject: true,
        dry: false
      }),
      new HtmlWebpackPlugin({
        filename: 'index.html',
        template: path.resolve(__dirname, './test/app/testapp.html'),
        inject: true
      })
    ]
  };

  return merge(commonConfig, config);
};

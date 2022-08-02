/*eslint-env node*/
const path = require('path');
const { merge } = require('webpack-merge');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const TerserPlugin = require('terser-webpack-plugin');
const commonConfig = require('./webpack.common.config');
const ESLintPlugin = require('eslint-webpack-plugin');
const webpack = require('webpack');

module.exports = function(env) {
  var developmentMode = env && 'development' in env;
  var useEslint = env && 'eslint' in env;
  var config = {
    entry: {
      appbg: './js/app_bg',
      appen: './js/app_en'
    },
    output: {
      path: path.resolve(__dirname, '../Eumis.Web.Host/App/'),
      filename: 'js/[name].[chunkhash].js'
    },
    resolve: {
      alias: {
        js: path.resolve(__dirname, './js')
      }
    },
    mode: developmentMode ? 'development' : 'production',
    devtool: developmentMode ? 'eval-source-map' : false,
    optimization: {
      splitChunks: {
        chunks: 'all'
      },
      runtimeChunk: false,
      minimizer: [
        // skip minification during development
        ...(developmentMode
          ? []
          : [
              new TerserPlugin({
                test: /\.js(\?.*)?$/i
              })
            ])
      ]
    },
    plugins: [
      new webpack.ProgressPlugin(),
      ...(useEslint ? [new ESLintPlugin()] : []),
      new CleanWebpackPlugin({
        cleanOnceBeforeBuildPatterns: [path.resolve(__dirname, '../Eumis.Web.Host/App/*')],
        dangerouslyAllowCleanPatternsOutsideProject: true,
        dry: false
      }),
      new webpack.EnvironmentPlugin({
        NODE_DEBUG: developmentMode ? 'development' : 'production'
      }),
      new CopyPlugin({
        patterns: [
          { from: 'login.cshtml' },
          { from: 'newPassword.cshtml' },
          { from: 'recoverPassword.cshtml' },
          { from: 'gdprDeclaration.cshtml' },
          { from: 'declaration.cshtml' }
        ]
      }),
      new HtmlWebpackPlugin({
        filename: 'browserNotSupported.cshtml',
        minify: false,
        template: path.resolve(__dirname, './browserNotSupported.cshtml'),
        inject: true,
        chunks: [] // skip javascripts
      }),
      new HtmlWebpackPlugin({
        filename: 'indexbg.cshtml',
        minify: false,
        template: path.resolve(__dirname, './index.cshtml'),
        env: { appModule: 'app.bg' },
        inject: true,
        excludeChunks: ['appen']
      }),
      new HtmlWebpackPlugin({
        filename: 'indexen.cshtml',
        minify: false,
        template: path.resolve(__dirname, './index.cshtml'),
        env: { appModule: 'app.en' },
        inject: true,
        excludeChunks: ['appbg']
      }),
      new HtmlWebpackPlugin({
        filename: 'layout.cshtml',
        minify: false,
        template: path.resolve(__dirname, './layout.cshtml'),
        inject: true,
        chunks: [] // skip javascripts
      })
    ]
  };

  return merge(commonConfig, config);
};

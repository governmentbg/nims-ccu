/*eslint-env node*/
const path = require('path');
const webpack = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const HtmlWebpackTagsPlugin = require('html-webpack-tags-plugin');
const DonePlugin = require('./tools/webpack-done-plugin');

module.exports = {
  target: 'web',
  plugins: [
    new HtmlWebpackTagsPlugin({
      tags: [
        'css/font-awesome/css/font-awesome.css',
        'css/bootstrap-datepicker/css/datepicker3.css',
        'css/blueimp-file-upload/css/jquery.fileupload-ui.css',
        'css/blueimp-file-upload/css/jquery.fileupload.css',
        'css/angular-bootstrap-nav-tree/dist/abn_tree.css',
        'css/bootstrap.css',
        'css/eumis.css',
        'css/select2.css',
        'css/style.css',
        'css/tabdrop.css',
        'css/uxp.css'
      ],
      append: false
    }),
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery',
      'window.jQuery': 'jquery'
    }),
    new CopyWebpackPlugin({
      patterns: [
        {
          from: './node_modules/font-awesome/css/font-awesome.css',
          to: 'css/font-awesome/css/font-awesome.css'
        },
        { from: './node_modules/font-awesome/fonts', to: 'css/font-awesome/fonts' },
        {
          from: './node_modules/bootstrap-datepicker/css/datepicker3.css',
          to: 'css/bootstrap-datepicker/css/datepicker3.css'
        },
        {
          from: './node_modules/blueimp-file-upload/css/jquery.fileupload-ui.css',
          to: 'css/blueimp-file-upload/css/jquery.fileupload-ui.css'
        },
        {
          from: './node_modules/blueimp-file-upload/css/jquery.fileupload.css',
          to: 'css/blueimp-file-upload/css/jquery.fileupload.css'
        },
        { from: './node_modules/blueimp-file-upload/img', to: 'css/blueimp-file-upload/img' },
        {
          from: './node_modules/angular-bootstrap-nav-tree/dist/abn_tree.css',
          to: 'css/angular-bootstrap-nav-tree/dist/abn_tree.css'
        },
        { from: './css', to: 'css' },
        { from: './fonts', to: 'fonts' },
        { from: './img', to: 'img' },
        { from: './templates', to: 'templates' }
      ]
    }),
    // Ignore all locale files of moment.js
    // https://github.com/jmblog/how-to-optimize-momentjs-with-webpack
    new webpack.IgnorePlugin({
      resourceRegExp: /^\.\/locale$/,
      contextRegExp: /moment$/
    }),
    new DonePlugin({ options: true })
  ],
  module: {
    rules: [
      {
        test: /\.js$/,
        use: [
          {
            loader: 'babel-loader'
          }
        ],
        exclude: /node_modules/
      },

      // pass all templates to the ngtemplate-loader which will add
      // them to the templateCache
      {
        test: /\.html$/,
        use: [`ngtemplate-loader?relativeTo=${path.resolve(__dirname)}`, 'raw-loader'],
        exclude: [path.resolve(__dirname, './test/app/testapp.html')]
      },

      // legacy libraries fixup
      {
        test: require.resolve('select2'),
        use: 'exports-loader?window.Select2'
      },
      {
        test: require.resolve('blueimp-file-upload/js/jquery.fileupload'),
        use: 'imports-loader?define=>false,exports=>false'
      },

      // legacy angular libraries fixup
      // remove this should you upgrade a package version and
      // it correctly exports its name as default
      {
        test: require.resolve('angular-ui-bootstrap'),
        use: "exports-loader?'ui.bootstrap'"
      },
      {
        test: require.resolve('angular-ui-sortable/src/sortable'),
        use: "exports-loader?'ui.sortable'"
      },
      {
        test: require.resolve('l10n-angular/build/l10n-with-tools'),
        use: ["exports-loader?'l10n-tools'", 'imports-loader?this=>window']
      },
      {
        test: require.resolve('angular-chart.js'),
        use: 'exports-loader?module.exports.name'
      },
      {
        test: require.resolve('angular-bootstrap-nav-tree'),
        use: ["exports-loader?'angularBootstrapNavTree'", 'imports-loader?this=>window']
      },
      {
        test: require.resolve('angular-ui-select2/src/select2'),
        use: "exports-loader?'ui.select2'"
      },
      {
        test: require.resolve('angular-ui-utils/modules/jq/jq'),
        use: "exports-loader?'ui.jq'"
      }
    ]
  }
};

/* eslint-disable no-console */
/* eslint-disable no-undef */
var findInFiles = require('find-in-files');

let currentDirectory = './js/main/modules/';
var folder = '';
var fileName = '';
var showFilename = undefined;
let textPattern = /text=/g;
let titlePattern = /title=/g;
let attributeTextPattern = 'text="([A-Za-z0-9 _]*)"';
let attributeTitlePattern = 'title="([A-Za-z0-9 _]*)"';

let processResources = function(results, resultArray, pattern) {
  for (var result in results) {
    var resultItem = results[result];
    if (!resultArray[result]) {
      resultArray[result] = [];
    }
    for (let k = 0; k < resultItem.count; k++) {
      var tmpString = resultItem.matches[k].replace(/"/, '');
      if (showFilename) {
        tmpString = tmpString.replace(/"$/, '');
      } else {
        tmpString = tmpString.replace(/"$/, ": '',");
      }
      resultArray[result].push(tmpString.replace(pattern, ''));
    }
  }
  return resultArray;
};

let printResult = function(arrayOfFiles) {
  for (let file in arrayOfFiles) {
    if (showFilename) {
      console.log(file);
    } else {
      console.log('//');
    }
    arrayOfFiles[file].forEach(e => console.log(e));
    console.log();
  }
};

// prepare arguments process.argv
process.argv.forEach(ind => {
  if (ind.replace('dir=') !== ind) {
    folder = ind.replace('dir=', '');
  }
  if (ind.replace('file=') !== ind) {
    fileName = ind.replace('file=', '');
    fileName = fileName.replace('.html', '');
  }
  if (ind.replace('show') !== ind) {
    showFilename = true;
  }
});

let workingPath = `${currentDirectory}${folder ? folder : ''}`;

processFiles = function(endsWith) {
  findInFiles
    .find(attributeTextPattern, workingPath, endsWith)
    .then(r => processResources(r, {}, textPattern))
    .then(r => {
      findInFiles
        .find(attributeTitlePattern, workingPath, endsWith)
        .then(e => processResources(e, r, titlePattern))
        .then(e => printResult(e));
    });
};

processFiles(fileName + '.html$');

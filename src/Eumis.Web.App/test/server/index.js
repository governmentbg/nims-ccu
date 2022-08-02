const path = require('path');
const express = require('express');
const filesMiddleware = require('./middleware/files');
const nomenclaturesMiddleware = require('./middleware/nomenclatures');

const app = express();

app.use(express.static(path.join(__dirname, '../build'), { index: ['index.html'] }));
app.use(filesMiddleware);
app.use(nomenclaturesMiddleware);

app.listen(4100, () => {
  // eslint-disable-next-line no-console
  console.log('Server started at port 4100');
});

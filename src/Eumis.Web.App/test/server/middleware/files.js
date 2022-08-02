const express = require('express');
const fileUpload = require('express-fileupload');
const path = require('path');

const router = express.Router();

router.use(fileUpload());
router.get('/files/newfilekey', function(req, res) {
  if (req.query.access_token !== 'newfiletoken') {
    return res.status(401).send('Wrong token');
  }
  res.download(path.join(__dirname, '../data/new.txt'));
});
router.get('/files/existingfilekey', function(req, res) {
  if (req.query.access_token !== 'existingfiletoken') {
    return res.status(401).send('Wrong token');
  }
  res.download(path.join(__dirname, '../data/existing.txt'));
});
router.post('/files/', function(req, res) {
  if (!req.files) {
    return res.status(400).send('No files were uploaded.');
  }

  return res.json({
    fileKey: 'newfilekey',
    accessToken: 'newfiletoken',
    size: 1024,
    hash: 'some_hash'
  });
});
router.get('/api/test/myparam/files/existingfilekey', function(req, res) {
  if (req.query.access_token !== 'testapptoken') {
    return res.status(401).send('Wrong token');
  }
  res.redirect('/files/existingfilekey?access_token=existingfiletoken');
});

module.exports = router;

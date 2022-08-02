const express = require('express');
const _ = require('lodash');

const router = express.Router();

const noms = {
  brands: [
    {
      nomValueId: 'vw',
      name: 'VW'
    },
    {
      nomValueId: 'audi',
      name: 'Audi'
    },
    {
      nomValueId: 'mercedes',
      name: 'Mercedes'
    }
  ],
  models: [
    {
      nomValueId: 'passat',
      name: 'Passat',
      brandId: 'vw'
    },
    {
      nomValueId: 'jetta',
      name: 'Jetta',
      brandId: 'vw'
    },
    {
      nomValueId: 'golf',
      name: 'Golf',
      brandId: 'vw'
    },
    {
      nomValueId: 'cclass',
      name: 'C-class',
      brandId: 'mercedes'
    }
  ]
};
router.get('/api/nomenclatures/:nom', function(req, res) {
  let items = noms[req.params.nom];

  const filters = _.omit(req.query, ['limit', 'offset', 'term']);
  if (_.keys(filters).length) {
    items = _.filter(items, filters);
  }

  if (req.query.term) {
    items = _.filter(items, i => i.name.indexOf(req.query.term) !== -1);
  }

  return res.json(items);
});

router.get('/api/nomenclatures/:nom/:nomValueId', function(req, res) {
  return res.json(_.find(noms[req.params.nom], { nomValueId: req.params.nomValueId }));
});

module.exports = router;

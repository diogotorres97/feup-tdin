const {
  Sell, Order, Book,
} = require('../models');
const db = require('../models/index');

const getTopBooks = async () => {
    const orders = await Order.findAll({
        attributes: ['bookId', [db.sequelize.fn('sum', db.sequelize.col('quantity')), 'total']],
        group : ['bookId', 'Book.id'],
        include: [
            { model: Book },
          ],
    });

    const sells = await Sell.findAll({
        attributes: ['bookId', [db.sequelize.fn('sum', db.sequelize.col('quantity')), 'total']],
        group : ['bookId', 'Book.id'],
        include: [
            { model: Book },
          ],
    });

    // eslint-disable-next-line
    let topBooks = {};
    sells.forEach((element) => {
        const sell = element.dataValues;
        if(topBooks[sell.bookId]) {
            topBooks[sell.bookId]['total'] += parseInt(sell.total);
        } else {
            topBooks[sell.bookId] = {total: parseInt(sell.total), book: sell.Book};
        };
    });

    orders.forEach((element) => {
        const order = element.dataValues;
        if(topBooks[order.bookId]) {
            topBooks[order.bookId]['total'] += parseInt(order.total);
        } else {
            topBooks[order.bookId] = {total: parseInt(order.total), book: order.Book};
        };
    });

    keysSorted = Object.keys(topBooks).sort(function(keyA,keyB){return topBooks[keyB].total - topBooks[keyA].total })

    // eslint-disable-next-line
    let topBooksSorted = [];
    keysSorted.forEach((key) => {
        topBooksSorted.push(topBooks[key]);
    });

    return topBooksSorted;
};

const getTotalSales = async () => {
    const ordersSum = 'SELECT sum("totalPrice") FROM "Orders"';
    const sellsSum = 'SELECT sum("totalPrice") FROM "Sells"';
    const query = `SELECT (${ordersSum}) + (${sellsSum}) as totalSales`;
    const result = await db.sequelize.query(query, {plain:true, type: db.sequelize.QueryTypes.SELECT});

    return result;

};

module.exports = {
  getTopBooks,
  getTotalSales,
};

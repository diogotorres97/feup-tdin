const messageType = {
  requestStock: 'request_stock',
  receiveStock: 'receive_stock',
  
  createClient: 'create_client',
  updateClient: 'update_client',
  updateBook: 'update_book',
  createOrder: 'create_order',
  updateOrder: 'update_order',
  createSell: 'create_sell',
  updateSell: 'update_sell',
  createReceiveStock: 'create_receiveStock',
  updateReceiveStock: 'update_receiveStock',
  printInvoice: 'print_invoice'
};

module.exports = {
  messageType,
};

const orderState = {
  waiting: 'WAITING',
  delivered: 'DELIVERED',
  dispatch: 'DISPATCH',
};

const messageType = {
  requestStock: 'request_stock',
  receiveStock: 'receive_stock',
};

orderState.toString = (state, stateDate) => {
  switch (state) {
    case orderState.waiting:
      return 'Waiting Expedition';
    case orderState.delivered:
      return `Dispatched at ${stateDate.toDateString()}`;
    case orderState.dispatch:
      return `Dispatch will occur at ${stateDate.toDateString()}`;
    default:
      throw new Error('Invalid order state specified');
  }
};

module.exports = {
  orderState,
  messageType,
};

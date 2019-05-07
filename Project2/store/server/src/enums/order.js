const orderState = {
  waiting: 'WAITING',
  delivered: 'DELIVERED',
  dispatch: 'DISPATCH',
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
};

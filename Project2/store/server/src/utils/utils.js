function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

class AsyncQueue {
  constructor() {
    this.publisherQueue = [];
    this.subscriberQueue = [];
  }

  push(value) {
    if (this.subscriberQueue.length > 0) {
      this.subscriberQueue.shift()(value);
    } else {
      this.publisherQueue.push(value);
    }
  }

  async pop() {
    if (this.publisherQueue.length > 0) {
      return Promise.resolve(this.publisherQueue.shift());
    }
    return new Promise(resolve => this.subscriberQueue.push(resolve));
  }
}

module.exports = {
  sleep,
  AsyncQueue,
};

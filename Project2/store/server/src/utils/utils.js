function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

class AsyncQueue {

  constructor() {
      this.publisherQueue = new Array();
      this.subscriberQueue = new Array();
  }

  push(value) {
      if (this.subscriberQueue.length > 0) {
          this.subscriberQueue.shift()(value);
      } else {
          this.publisherQueue.push(value);
      }
  }

  async pop() {
      if(this.publisherQueue.length > 0) {
          return Promise.resolve(this.publisherQueue.shift());
      } else {
          return new Promise(resolve => this.subscriberQueue.push(resolve));
      }
  }
}

module.exports = {
  sleep,
  AsyncQueue
};

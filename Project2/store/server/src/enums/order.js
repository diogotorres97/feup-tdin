const orderState = {
  waiting: 'WAITING',
  delivered: 'DELIVERED',
  dispatch: 'DISPATCH',
};

const messageType = {
  requestStock: 'request_stock',
  receiveStock: 'receive_stock',
};

// TODO: Create toString with handle with date to database :D

// orderState.toString = () => {

// }

// orderState.fromString = () => {

// }

// export namespace ActivityTypeNs {
//     export function toString(actionType: ActivityType): string {
//         switch (actionType) {
//             case ActivityType.Documents:
//                 return 'Documents';
//             case ActivityType.Profile:
//                 return 'Profile';
//             case ActivityType.FundingMatch:
//                 return 'FundingMatch';
//             case ActivityType.Feedback:
//                 return 'Feedback';
//             default:
//                 throw new Error('Invalid activity type enum specified');
//         }
//     }

//     export function fromString(aActionType: string): ActivityType | undefined {
//         if (!aActionType) {
//             return;
//         }

//         switch (aActionType) {
//             case 'Documents':
//                 return ActivityType.Documents;
//             case 'Profile':
//                 return ActivityType.Profile;
//             case 'FundingMatch':
//                 return ActivityType.FundingMatch;
//             case 'Feedback':
//                 return ActivityType.Feedback;
//             default:
//                 throw new Error('Invalid activity type specified');
//         }
//     }
// }

module.exports = {
  orderState,
  messageType,
};

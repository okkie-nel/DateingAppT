export interface Message {
    id: number;
    senderId: number;
    senderKnownAS: string;
    senderPhotoUrl: string;
    recipientid: number;
    recipientKnownAs: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;

}


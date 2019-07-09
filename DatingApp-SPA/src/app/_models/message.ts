export interface Message {
    id: number;
    senderId: number;
    senderKnownAS: string;
    senderPhotoUrl: string;
    RecipientId: number;
    recipientKnownAs: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;

}


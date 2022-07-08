export interface KeyValuePair {
    id: string,
    name: string
}

export interface Contact {
    name: string;
    phone: string;
    email: string;
}

export interface Vehicle {
    id: string;
    model: KeyValuePair;
    make: KeyValuePair;
    isRegistered: boolean;
    features: KeyValuePair[];
    contact: Contact;
    lastUpdate: string;
}

export interface SaveVehicle {
    id: string;
    modelId?: string;
    makeId: string;
    isRegistered: boolean;
    features: string[];
    contact: Contact;
}
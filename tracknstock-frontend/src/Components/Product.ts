export interface IProduct {
    id: number;
    name: string;
    boughtPrice: number;
    unitPrice: number;
    quantity: number;
    totalPrice: number;
}

export const dummyProductList: IProduct[] = [
    {
    id: 1,
    name: "Product A",
    boughtPrice: 50,
    unitPrice: 70,
    quantity: 10,
    totalPrice: 700
    },
]

export enum PageEnum {
    list,
    add
}
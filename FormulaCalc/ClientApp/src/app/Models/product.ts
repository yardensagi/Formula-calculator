import { Ingeredient } from './ingredient';

export class Product {
    productID: number;
    name: string;
    price: number;
    mass: number;
    ingredients: Ingeredient[];
    instructions: string;
}

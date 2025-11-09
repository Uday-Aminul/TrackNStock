import {IProduct} from "./Product"
import "./ProductListStyle.css"

type Products={
    list:IProduct[]
}

const ProductList=(products:Products)=>{

    const {list}=products;

    return(
        <div>Available Products:
            <table>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Bought Price</th>
                <th>Unit Price</th>
                <th>Quantity</th>
                <th>Total</th>
                <th>Actions</th>
            </tr>

            {list.map(product=>{
                return(
                    <tr key={product.id}>
                        <td>{product.id}</td>
                        <td>{product.name}</td>
                        <td>{product.boughtPrice}</td>
                        <td>{product.unitPrice}</td>
                        <td>{product.quantity}</td>
                        <td>{product.totalPrice}</td>
                        <td>
                            <div>
                                <input type="button" value="Edit"/>
                                <input type="button" value="Delete"/>
                            </div>    
                        </td>
                    </tr>
                );
            })}                
            </table>
        </div>
    );
};
export default ProductList;
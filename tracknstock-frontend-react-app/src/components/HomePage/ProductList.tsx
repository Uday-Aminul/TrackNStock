import axios from "axios";
import type { Product } from "../../models/Product";

interface ProductListProps {
  products: Product[];
  onProductDeleted: () => void;
  onEditProductClick: (id: number) => void;
}

function ProductList({
  products,
  onProductDeleted,
  onEditProductClick,
}: ProductListProps) {
  const onDeleteClickHandle = async (id: number) => {
    if (!window.confirm("Are you sure you want to delete this product?"))
      return;

    try {
      await axios.delete(`http://localhost:5113/api/products/${id}`);
      alert("Product deleted successfully!");
      await onProductDeleted(); // refresh list
    } catch (err) {
      console.error(err);
      alert("Failed to delete product");
    }
  };

  const onSellClickHandle = async (id: number) => {
    if (!window.confirm("Mark this product as sold?")) return;

    try {
      await axios.patch(`http://localhost:5113/api/products/${id}/sell`);
      alert("Product sold successfully!");

      // Optional: remove the product from the list immediately
      await onProductDeleted(); // refresh list
    } catch (err) {
      console.error(err);
      alert("Failed to sell product");
    }
  };

  return (
    <table>
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Bought Price</th>
          <th>Unit Price</th>
          <th>Quantity</th>
          <th>Total</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {products.map((product) => (
          <tr key={product.id}>
            <td>{product.id}</td>
            <td>{product.name}</td>
            <td>{product.boughtPrice}</td>
            <td>{product.unitPrice}</td>
            <td>{product.quantity}</td>
            <td>{product.unitPrice * product.quantity}</td>
            <td>
              <div>
                <button
                  className="row-btn edit"
                  onClick={() => onEditProductClick(product.id)}
                >
                  Edit
                </button>
                <button
                  className="row-btn sell"
                  title="Sell"
                  onClick={() => onSellClickHandle(product.id)}
                >
                  Sell
                </button>
                <button
                  className="row-btn delete"
                  onClick={() => onDeleteClickHandle(product.id)}
                >
                  Delete
                </button>
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
export default ProductList;

import { useState } from "react";
import "../styles/AddProductPage.css";
import axios from "axios";

interface AddProductPageProps {
  onBackButtonClickHandle: () => void;
  onProductAdded: () => void;
}

function AddProductPage({
  onBackButtonClickHandle,
  onProductAdded,
}: AddProductPageProps) {
  const [name, setName] = useState("");
  const [boughtPrice, setBoughtPrice] = useState(0);
  const [unitPrice, setUnitPrice] = useState(0);
  const [quantity, setQuantity] = useState(0);

  const onSaveProductClickHandle = async (e: React.FormEvent) => {
    e.preventDefault();
    const newProduct = {
      name,
      boughtPrice,
      unitPrice,
      quantity,
    };

    try {
      await axios.post("http://localhost:5113/api/products", newProduct);

      alert("Product added successfully!");

      // optional: reset fields
      setName("");
      setBoughtPrice(0);
      setUnitPrice(0);
      setQuantity(0);

      await onProductAdded();
      onBackButtonClickHandle();
    } catch (err) {
      console.error(err);
      alert("Failed to add product");
    }
  };

  return (
    <div className="addProductForm">
      <div>
        <h3>Add details for the new product:</h3>
      </div>

      <form onSubmit={onSaveProductClickHandle}>
        <div>
          <label>Name : </label>
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </div>

        <div>
          <label>Bought Price : </label>
          <input
            type="number"
            value={boughtPrice}
            onChange={(e) => setBoughtPrice(Number(e.target.value))}
          />
        </div>

        <div>
          <label>Unit Price : </label>
          <input
            type="number"
            value={unitPrice}
            onChange={(e) => setUnitPrice(Number(e.target.value))}
          />
        </div>

        <div>
          <label>Quantity : </label>
          <input
            type="number"
            value={quantity}
            onChange={(e) => setQuantity(Number(e.target.value))}
          />
        </div>

        <div>
          <input type="button" value="Back" onClick={onBackButtonClickHandle} />
          <input type="submit" value="Add Product" />
        </div>
      </form>
    </div>
  );
}
export default AddProductPage;

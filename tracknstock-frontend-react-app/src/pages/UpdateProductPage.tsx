import { useState, useEffect } from "react";
import axios from "axios";
import type { Product } from "../models/Product";
import "../styles/AddProductPage.css";

interface UpdateProductPageProps {
  productId: number;
  onBackButtonClickHandle: () => void;
  onProductUpdated: () => void;
}

function UpdateProductPage({
  productId,
  onBackButtonClickHandle,
  onProductUpdated,
}: UpdateProductPageProps) {
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5113/api/products/${productId}`
        );
        setProduct(response.data);
        setLoading(false);
      } catch (err) {
        console.error(err);
        alert("Failed to fetch product details");
        setLoading(false);
      }
    };
    fetchProduct();
  }, [productId]);

  const onSaveClickHandle = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!product) return;

    try {
      await axios.put(
        `http://localhost:5113/api/products/${product.id}`,
        product
      );
      alert("Product updated successfully!");
      await onProductUpdated();
      onBackButtonClickHandle();
    } catch (err) {
      console.error(err);
      alert("Failed to update product");
    }
  };

  if (loading) return <div>Loading product...</div>;
  if (!product) return <div>Product not found</div>;

  return (
    <div className="addProductForm">
      <h3>Update Product:</h3>
      <form onSubmit={onSaveClickHandle}>
        <div>
          <label>Name : </label>
          <input
            type="text"
            value={product.name}
            onChange={(e) => setProduct({ ...product, name: e.target.value })}
          />
        </div>
        <div>
          <label>Bought Price : </label>
          <input
            type="number"
            value={product.boughtPrice}
            onChange={(e) =>
              setProduct({ ...product, boughtPrice: Number(e.target.value) })
            }
          />
        </div>
        <div>
          <label>Unit Price : </label>
          <input
            type="number"
            value={product.unitPrice}
            onChange={(e) =>
              setProduct({ ...product, unitPrice: Number(e.target.value) })
            }
          />
        </div>
        <div>
          <label>Quantity : </label>
          <input
            type="number"
            value={product.quantity}
            onChange={(e) =>
              setProduct({ ...product, quantity: Number(e.target.value) })
            }
          />
        </div>
        <div>
          <input type="button" value="Back" onClick={onBackButtonClickHandle} />
          <input type="submit" value="Update Product" />
        </div>
      </form>
    </div>
  );
}

export default UpdateProductPage;

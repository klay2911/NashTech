import React, { useEffect, useState } from 'react';
import { getBook } from '../../services/books.service';
import { useParams, Link } from 'react-router-dom';

const DetailsBook = ({}) => {
    const [book, setBook] = useState(null);
    const { id } = useParams(); 

    useEffect(() => {
        const fetchBook = async () => {
            try {
                const response = await getBook(id);
                setBook(response?.data);
            } catch (error) {
                console.error("Error fetching book:", error);
            }
        };

        fetchBook();
    }, [id]);

    return (
        <div className="flex flex-col">
            <div className="flex mb-4">
                <div className="w-1/2">
                    <img src={book?.coverPah ? `${process.env.REACT_APP_API_BASE_URL}${book?.coverPah}` : 'https://localhost:44352/covers/Cover.png'} alt={book?.title} className="w-full h-40 object-cover mb-4" />
                </div>
                <div className="w-1/2 pl-4">
                    <h2 className="text-xl font-bold mb-2">{book?.title}</h2>
                    <p className="mb-2"><strong>Author:</strong> {book?.author}</p>
                    <p className="mb-2"><strong>ISBN:</strong> {book?.isbn}</p>
                    <p className="mb-2"><strong>Description:</strong> {book?.description}</p>
                    {/* <p className="mb-2"><strong>Category:</strong> {book?.category?.name}</p> */}
                </div>
            </div>
            <div className="flex justify-center mt-10">
                <Link to="/borrowingbooks" className="bg-gray-500 hover:bg-gray-600 text-white font-bold py-2 px-4 rounded">
                    Back
                </Link>
            </div>
        </div>
        
    );
};

export default DetailsBook;
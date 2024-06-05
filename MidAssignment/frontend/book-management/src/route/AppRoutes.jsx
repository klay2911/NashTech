import React, { Suspense } from 'react';
import {useRoutes} from 'react-router-dom';
import {Home, NotFound, Books, CreateBook, UpdateBook, CreateCategory, Requests, WaitingRequests} from '../pages';
import {path} from './routeContants';
import { RequiredAuth } from '../components';
import DetailsRequest from '../pages/Librarian/BorrowingRequest/DetailsRequest';

const LoginLazy = React.lazy(() => import('../pages/Login'));
const HomeLazy = React.lazy(() => import('../pages/Home'));
const BooksLazy = React.lazy(() => import('../pages/Books/Books'));
const CategoriesLazy = React.lazy(() => import('../pages/Categories/Categories'));
const UpdateCategoryLazy = React.lazy(() => import('../pages/Categories/UpdateCategory'));
const ProfileLazy = React.lazy(() => import('../pages/Profile'));


export const AppRoutes = () => {
  const elements = useRoutes(
    [
      {
        path: path.home, element: <Suspense><HomeLazy /></Suspense>, errorElement: <NotFound/>
      },
      {
        path: path.requestDetails, element: <RequiredAuth><DetailsRequest/></RequiredAuth>, errorElement: <NotFound/>
      },
      {
        path: path.books, element:<RequiredAuth><Suspense><BooksLazy /></Suspense></RequiredAuth> , errorElement: <NotFound/>
      },
      
      {
        path: path.bookCreate, element:<CreateBook/> , errorElement: <NotFound/>
      },
      {
        path: path.bookEdit, element:<UpdateBook/> , errorElement: <NotFound/>
      },
      {
        path: path.categories, element:<Suspense> <CategoriesLazy /> </Suspense> , errorElement: <NotFound/>
      },
      {
        path: path.categoryCreate, element:<CreateCategory/> , errorElement: <NotFound/>
      },
      {
        path: path.categoryEdit, element:<Suspense><UpdateCategoryLazy /></Suspense> , errorElement: <NotFound/>
      },
      {
        path: path.requests, element:<WaitingRequests/> , errorElement: <NotFound/>
      },
      {
        path: path.profile, element:<RequiredAuth><Suspense><ProfileLazy /></Suspense> </RequiredAuth> , errorElement: <NotFound/>
      },
      {
        path: path.login, element:<Suspense><LoginLazy /></Suspense> , errorElement: <NotFound/>
        },
      {
        path: "*",
        element: <NotFound />
      },
    ]);
  return elements;
}
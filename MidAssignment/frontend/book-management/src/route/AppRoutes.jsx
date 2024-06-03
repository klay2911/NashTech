import React, { Suspense } from 'react';
import {useRoutes} from 'react-router-dom';
import {Home, NotFound, Books, CreateBook, UpdateBook, Profile, CreateCategory, UpdateCategory} from '../pages';
import {path} from './routeContants';
import { RequiredAuth } from '../components';

const LoginLazy = React.lazy(() => import('../pages/Login'));
const HomeLazy = React.lazy(() => import('../pages/Home'));
const CategoriesLazy = React.lazy(() => import('../pages/Categories/Categories'));
const UpdateCategoryLazy = React.lazy(() => import('../pages/Categories/UpdateCategory'));
const ProfileLazy = React.lazy(() => import('../pages/Profile'));


export const AppRoutes = () => {
  const elements = useRoutes(
    [
      {
        path: path.home, element: <Suspense><HomeLazy /></Suspense>, errorElement: <NotFound/>
      },
      // {
      // path: path.posts, element: <Suspense><BooksLazy /></Suspense>, errorElement: <NotFound/>
      // },
      {
        path: path.books, element:<Books/> , errorElement: <NotFound/>
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
        path: path.categoryEdit, element: <UpdateCategory />, errorElement: <NotFound/>
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
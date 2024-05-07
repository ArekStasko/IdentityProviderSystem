import { Route, Routes } from 'react-router-dom';
import RoutingConstants from './routingConstants';
import React from 'react';
import MainPage from "../../pages/mainPage/MainPage";
import Register from "../../components/register/Register";
import Login from "../../components/login/Login";
import IdpManager from "../../pages/idpManager/IdpManager";

export const MainRouting = () => (
    <Routes>
        <Route
            path={RoutingConstants.root}
            element={<IdpManager/>}
        />
        <Route
            path={RoutingConstants.login}
            element={<MainPage><Login/></MainPage>}
        />
        <Route
            path={RoutingConstants.register}
            element={<MainPage><Register /></MainPage>}
        />
    </Routes>
);

export default MainRouting;
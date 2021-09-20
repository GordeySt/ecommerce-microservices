const ReactRefreshWebpackPlugin = require('@pmmmwh/react-refresh-webpack-plugin');
const webpackMockServer = require('webpack-mock-server');
const webpack = require('webpack');
const path = require('path');

const dotenv = require('dotenv').config({
    path: path.join(__dirname, './.env.dev'),
});

module.exports = {
    mode: 'development',
    devServer: {
        hot: true,
        open: true,
        compress: true,
        publicPath: '/',
        historyApiFallback: true,
        before: (app) =>
            webpackMockServer.use(app, {
                tsConfigFileName: 'tsconfig.json',
            }),
    },
    devtool: 'cheap-module-source-map',
    plugins: [
        new webpack.DefinePlugin({
            'process.env': JSON.stringify(dotenv.parsed),
            'process.env.name': JSON.stringify('dev'),
        }),
        new ReactRefreshWebpackPlugin(),
    ],
};

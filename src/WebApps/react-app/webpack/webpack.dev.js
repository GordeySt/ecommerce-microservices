﻿const webpack = require('webpack')
const ReactRefreshWebpackPlugin = require('@pmmmwh/react-refresh-webpack-plugin')
const webpackMockServer = require('webpack-mock-server')

module.exports = {
    mode: 'development',
    devServer: {
        hot: true,
        open: true,
        compress: true,
        historyApiFallback: true,
        before: (app) =>
            webpackMockServer.use(app, {
                entry: [],
                tsConfigFileName: 'mock/tsconfig.json',
            }),
    },
    devtool: 'cheap-module-source-map',
    plugins: [
        new webpack.DefinePlugin({
            'process.env.name': JSON.stringify('devConfig'),
        }),
        new ReactRefreshWebpackPlugin(),
    ],
}

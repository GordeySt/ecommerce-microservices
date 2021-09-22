const webpack = require('webpack');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
const path = require('path');

const dotenv = require('dotenv').config({
    path: path.join(__dirname, './.env.prod'),
});

module.exports = {
    mode: 'production',
    devtool: 'source-map',
    plugins: [
        new webpack.DefinePlugin({
            'process.env': JSON.stringify(dotenv.parsed),
            'process.env.name': JSON.stringify('prod'),
        }),
        new BundleAnalyzerPlugin(),
    ],
};

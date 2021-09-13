import webpackMockServer from 'webpack-mock-server';

export default webpackMockServer.add((app) => {
    app.get('/health-check', (req, res) => {
        res.json('health');
    });
});

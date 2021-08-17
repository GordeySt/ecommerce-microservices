import { createStyles, makeStyles } from '@material-ui/core'
import CircularProgress from '@material-ui/core/CircularProgress'

const useStyles = makeStyles(() =>
    createStyles({
        root: {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            height: '100vh',
        },
    })
)

export const Loader = () => {
    const classes = useStyles()

    return (
        <div className={classes.root}>
            <CircularProgress color="secondary" />
        </div>
    )
}
